using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;

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
        try
        {
            command.ClientId = id;
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class AddAttachedmentsCommand : IRequest<Result<Unit>>
    {
        public int ClientId { get; set; }

        public List<Document> Attachments { get; set; }

        public class Document
        {
            public IFormFile Attachment { get; set; }
            public string DocumentType { get; set; }
        }
    }

    public class Handler : IRequestHandler<AddAttachedmentsCommand, Result<Unit>>
    {
        private readonly Cloudinary _cloudinary;
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context, IOptions<CloudinaryOptions> options)
        {
            _context = context;
            var account = new Account(
                options.Value.Cloudname,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<Result<Unit>> Handle(AddAttachedmentsCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients
                .Include(x => x.ClientDocuments)
                .FirstOrDefaultAsync(
                    x => x.Id == request.ClientId &&
                         x.RegistrationStatus == "Pending registration", cancellationToken);

            if (existingClient == null)
            {
                return Result<Unit>.Failure(ClientErrors.NotFound());
            }

            foreach (var documents in request.Attachments.Where(documents => documents.Attachment.Length > 0))
            {
                await using var stream = documents.Attachment.OpenReadStream();

                var attachmentsParams = new ImageUploadParams
                {
                    File = new FileDescription(documents.Attachment.FileName, stream),
                    PublicId = $"{HttpUtility.UrlEncode(existingClient.BusinessName)}/{documents.Attachment.FileName}"
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
            return Result<Unit>.Success(Unit.Value, "Attachments uploaded successfully");
        }
    }
}