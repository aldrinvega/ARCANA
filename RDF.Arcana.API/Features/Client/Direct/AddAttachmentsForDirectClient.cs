using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Abstractions.Storage;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Direct;

[Route("api/RegularRegistration")]
[ApiController]
public class AddAttachmentsForDirectClient : ControllerBase
{
    private readonly IMediator _mediator;

    public AddAttachmentsForDirectClient(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("AddAttachmentsForDirectClient/{id}")]
    public async Task<IActionResult> AddRequirementForRegularClients([FromForm]AddAttachmentsForRegularClientCommand command,
        [FromRoute] int id)
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

    public class AddAttachmentsForRegularClientCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public List<Files> Attachments { get; set; }

        public class Files
        {
            public IFormFile Attachment { get; set; }
            public string DocumentType { get; set; }
        }
    }

    public class Handler : IRequestHandler<AddAttachmentsForRegularClientCommand, Result>
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

        public async Task<Result> Handle(AddAttachmentsForRegularClientCommand request,
            CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients
                .Include(x => x.ClientDocuments)
                .FirstOrDefaultAsync(
                    x => x.Id == request.ClientId, cancellationToken);

            if (existingClient == null)
            {
                throw new ClientIsNotFound(request.ClientId);
            }

            if (request.Attachments == null)
                return DirectRegistrationErrors.NoAttachmentFound();
            
            var uploadTasks = new List<Task>();
            foreach (var documents in request.Attachments.Where(documents => documents.Attachment.Length > 0))
            {
                //var strean = documents.Attachment.OpenReadStream();

                //var fileId = await _blobService.UploadAsync(strean, documents.Attachment.ContentType, cancellationToken);
                await using var stream = documents.Attachment.OpenReadStream();

                var attachmentsParams = new ImageUploadParams
                {
                    File = new FileDescription(documents.Attachment.FileName, stream),
                    PublicId =
                        $"{HttpUtility.UrlEncode(existingClient.BusinessName)}/{documents.Attachment.FileName}"
                };

                var attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams);

                var attachments = new ClientDocuments
                {
                    DocumentPath = attachmentsUploadResult.SecureUrl.ToString(),
                    ClientId = existingClient.Id,
                    DocumentType = documents.DocumentType
                };

                await _context.ClientDocuments.AddAsync(attachments, cancellationToken);
                existingClient.RegistrationStatus = Status.UnderReview;
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            await Task.WhenAll(uploadTasks);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}