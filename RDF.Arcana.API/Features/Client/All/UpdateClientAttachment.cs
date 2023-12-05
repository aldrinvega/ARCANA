using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Client.All;
[Route("api/Client"), ApiController]

public class UpdateClientAttachment : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateClientAttachment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateClientAttachments/{id:int}")]
    public async Task<IActionResult> UpdateDocuments([FromForm]UpdateAttachmentsCommand command, [FromRoute] int id)
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UpdateAttachmentsCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public List<ClientAttachments> Attachments { get; set; }

        public class ClientAttachments
        {
            public IFormFile Attachment { get; set; }
            public string DocumentType { get; set; }
        }
    }

    public class Handler : IRequestHandler<UpdateAttachmentsCommand, Result>
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

        public async Task<Result> Handle(UpdateAttachmentsCommand request, CancellationToken cancellationToken)
        {
            var clientAttachments = await _context.ClientDocuments
                .Include(client => client.Clients)
                .Where(cd => cd.ClientId == request.ClientId)
                .ToListAsync(cancellationToken);
            
            var uploadTasks = new List<Task>();

            if (request.Attachments != null)
            {
                    /*uploadTasks.Add(Task.Run(async () =>
                    {*/
                        foreach (var newAttachment in request.Attachments)
                        {
                            if (newAttachment.Attachment == null || newAttachment.Attachment.ContentType.Contains("text/plain"))
                            {
                                // If the Attachment is null or plain-text (indicating it could be a URL), skip current loop iteration
                                continue;
                            }
                            // If the new attachment is a file, upload it and update the relevant record in the database.
                            if (newAttachment.Attachment.Length > 0)
                            {
                                await using var stream = newAttachment.Attachment.OpenReadStream();

                                var attachmentsParams = new ImageUploadParams
                                {
                                    File = new FileDescription(newAttachment.Attachment.FileName, stream),
                                    PublicId =
                                        $"{HttpUtility.UrlEncode(clientAttachments.First().Clients.BusinessName)}/{newAttachment.Attachment.FileName}"
                                };

                                var attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams);


                                var clientDocument =
                                    clientAttachments.FirstOrDefault(
                                        cd => cd.DocumentType == newAttachment.DocumentType);

                                if (clientDocument == null)
                                {
                                    // If no existing ClientDocument of the new attachment's DocumentType is found, create and add it
                                    clientDocument = new ClientDocuments
                                    {
                                        DocumentType = newAttachment.DocumentType, 
                                        DocumentPath = attachmentsUploadResult.SecureUrl.ToString(), 
                                        ClientId = request.ClientId
                                    };

                                    _context.ClientDocuments.Add(clientDocument);
                                }
                                else
                                {
                                    // If an existing ClientDocument already exists, update DocumentPath
                                    clientDocument.DocumentPath = attachmentsUploadResult.SecureUrl.ToString();
                                    _context.ClientDocuments.Update(clientDocument);
                                }
                            }
                            // If the new attachment is a link, ignore it.
                            

                            await _context.SaveChangesAsync(cancellationToken);
                        }
                    /*}, cancellationToken));
                    await Task.WhenAll(uploadTasks);*/
                    return Result.Success();
            }

            return Result.Success();
        }
    }
}