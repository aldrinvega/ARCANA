using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.All
{

    [Route("api/attachments"), ApiController]
    public class GetClientAttachmentsById : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetClientAttachmentsById(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>Get(int id)
        {
            try
            {
                var query = new GetClientsAttachmentsByIdQuery
                {
                    ClientId = id
                };

                var result = await _mediator.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        public class GetClientsAttachmentsByIdQuery : IRequest<Result>
        { 
            public int ClientId { get; set; }
        }
        public class ClientAttachmentsResult
        {
            public IEnumerable<Attachment> Attachments { get; set; }
            public class Attachment
            {
                public int DocumentId { get; set; }
                public string DocumentLink { get; set; }
                public string DocumentType { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetClientsAttachmentsByIdQuery, Result>
        {
            private readonly ArcanaDbContext _conteext;

            public Handler(ArcanaDbContext conteext)
            {
                _conteext = conteext;
            }

            public async Task<Result> Handle(GetClientsAttachmentsByIdQuery request, CancellationToken cancellationToken)
            {
                var attachment = await _conteext.ClientDocuments
                    .Where(cd => cd.ClientId == request.ClientId)
                    .ToListAsync();

                var attachments = new ClientAttachmentsResult
                {
                    Attachments = attachment.Select(at => new ClientAttachmentsResult.Attachment
                    {
                        DocumentId = at.Id,
                        DocumentLink = at.DocumentPath,
                        DocumentType = at.DocumentType
                    })
                };

                return Result.Success(attachments);
            }
        }
    }
}
