using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.New_Doamin;
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
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddAttachedmentsCommand request, CancellationToken cancellationToken)
            {
                var exisitingClient = await _context.Clients
                    .Include(x => x.ClientDocuments)
                    .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken);

                if (exisitingClient == null)
                {
                    throw new ClientIsNotFound();
                }

                foreach (var documents in request.AttachMents)
                {
                    if (documents != null)
                    {
                        var savePath = Path.Combine($@"F:\images\{exisitingClient.BusinessName}", documents.FileName);

                        var directory = Path.GetDirectoryName(savePath);
                        if (directory != null && !Directory.Exists(directory))
                            Directory.CreateDirectory(directory);

                        await using var stream = System.IO.File.Create(savePath);
                        await documents.CopyToAsync(stream, cancellationToken);

                        var docuemtns = new ClientDocuments
                        {
                            DocumentPath = savePath,
                            ClientId = request.ClientId
                        };

                        await _context.ClientDocuments.AddAsync(docuemtns);

                        //exisitingClient.DocumentPath = savePath;
                        //exisitingClient.ClientId = request.ClientId;
                        //exisitingClient.DocumentType = documents.DocumentType;
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
