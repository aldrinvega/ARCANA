using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Register;

[Route("api/Registration")]
[ApiController]
public class AddAttachments : ControllerBase
{
    private readonly IMediator _mediator;

    public AddAttachments(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("AddAttachments/{id}")]
    public async Task<IActionResult> AddRequirements([FromForm] AddAttachedmentsCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ClientId = id;
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Attached added successfully");
            response.Status = StatusCodes.Status200OK;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Messages.Add($"{ex.Message}");
            response.Status = StatusCodes.Status404NotFound;
            return Conflict(response);
        }
    }

    public class AddAttachedmentsCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }

        public List<Document> Attachments { get; set; }

        public class Document
        {
            public IFormFile Attachment { get; set; }
            public string DocumentType { get; set; }
        }
    }

    public class Handler : IRequestHandler<AddAttachedmentsCommand, Unit>
    {
        private readonly Cloudinary _cloudinary;
        private readonly DataContext _context;

        public Handler(DataContext context, IOptions<CloudinarySettings> config)
        {
            _context = context;
            var account = new Account(
                config.Value.Cloudname,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<Unit> Handle(AddAttachedmentsCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients
                                     .Include(x => x.ClientDocuments)
                                     .FirstOrDefaultAsync(
                                         x => x.Id == request.ClientId &&
                                              x.RegistrationStatus == "Pending registration", cancellationToken) ??
                                 throw new ClientIsNotFound(request.ClientId);


            foreach (var documents in request.Attachments.Where(documents => documents.Attachment.Length > 0))
            {
                await using var stream = documents.Attachment.OpenReadStream();

                var attachmentsParams = new ImageUploadParams
                {
                    File = new FileDescription(documents.Attachment.FileName, stream),
                    PublicId = $"{existingClient.BusinessName}/{documents.Attachment.FileName}"
                };

                var attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams);

                var attachments = new ClientDocuments
                {
                    DocumentPath = attachmentsUploadResult.SecureUrl.ToString(),
                    ClientId = existingClient.Id,
                    DocumentType = documents.DocumentType
                };

                await _context.ClientDocuments.AddAsync(attachments, cancellationToken);
                existingClient.RegistrationStatus = "Under review";
                await _context.SaveChangesAsync(cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}