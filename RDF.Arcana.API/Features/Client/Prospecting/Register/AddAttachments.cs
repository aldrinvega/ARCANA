using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Register
{
    [Route("api/Registration")]
    [ApiController]
    public class AddAttachments : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddAttachments(IMediator mediator)
        {
            _mediator = mediator;
        }

        public class AddAttachedmentsCommand : IRequest<Unit>
        {
            public int ClientId { get; set; }

            public List<IFormFile> AttachMents { get; set; }


            ////public class Attachments
            ////{ 
            ////    public IFormFile Document { get; set; }
            ////}

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

            //private Dictionary<string, string> LoadDocumentTypeMappings()
            //{
            //    // Load mappings from the configuration file or database
            //    // In this example, loading from a JSON file
            //    string configFileContent = File.ReadAllText("documentTypeMappings.json");
            //    var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(configFileContent);
            //    return config;
            //}

            //private string GetDocumentTypeForAttachment(IFormFile attachment)
            //{
            //    var documentTypeMappings = LoadDocumentTypeMappings();

            //    // Get the file name
            //    string fileName = Path.GetFileNameWithoutExtension(attachment.FileName);

            //    // Check if any mapping matches the file name
            //    foreach (var mapping in documentTypeMappings)
            //    {
            //        if (fileName.StartsWith(mapping.Key, StringComparison.OrdinalIgnoreCase))
            //        {
            //            return mapping.Value;
            //        }
            //    }

            //    // Default to "Unknown Document"
            //    return "Unknown Document";
            //}

            public async Task<Unit> Handle(AddAttachedmentsCommand request, CancellationToken cancellationToken)
            {

                var exisitingClient = await _context.Clients
                    .Include(x => x.ClientDocuments)
                    .FirstOrDefaultAsync(
                    x => x.Id == request.ClientId &&
                    x.RegistrationStatus == "Released", cancellationToken) ?? throw new ClientIsNotFound();

                foreach (var documents in request.AttachMents)
                {
                    if (documents.Length > 0)
                    {
                        await using var stream = documents.OpenReadStream();

                        var attachedmenetsParams = new ImageUploadParams
                        {
                            File = new FileDescription(documents.FileName, stream),
                            PublicId = $"{exisitingClient.BusinessName}/{documents.FileName}"
                        };

                        var attachmenetsUploadResult = await _cloudinary.UploadAsync(attachedmenetsParams);

                        var attachments = new ClientDocuments
                        {
                            DocumentPath = attachmenetsUploadResult.SecureUrl.ToString(),
                            ClientId = exisitingClient.Id,
                        };

                        await _context.ClientDocuments.AddAsync(attachments, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }

                exisitingClient.RegistrationStatus = "Registered";
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }

        [HttpPut("AddAttachedments/{id}")]
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
    }
}
