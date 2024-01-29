using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Freebies;

namespace RDF.Arcana.API.Features.Client.All
{
    [Route("api/freebies")]
    public class GetClientFreebiesById : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetClientFreebiesById(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var command = new GetClientFreebiesByIdCommand
                {
                    ClientId = id
                };

                var result = await _mediator.Send(command);     

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

        public class GetClientFreebiesByIdCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
        }

        public class ClientFreebieResult
        {
            public ICollection<FreebiesCollection> Freebies { get; set; }
            public class FreebiesCollection
            {
                public int? TransactionNumber { get; set; }
                public string Status { get; set; }
                public string ESignature { get; set; }
                public int? FreebieRequestId { get; set; }
                public IEnumerable<Items> Freebies { get; set; }
            }

            public class Items
            {
                public int? Id { get; set; }
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public string Uom { get; set; }
                public int? Quantity { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetClientFreebiesByIdCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetClientFreebiesByIdCommand request, CancellationToken cancellationToken)
            {
                var freebies = await _context.FreebieRequests
                    .Include(x => x.FreebieItems)
                    .ThenInclude(x => x.Items)
                    .ThenInclude(x => x.Uom)
                    .Where(x => x.ClientId == request.ClientId)
                    .ToListAsync();

                if(freebies is null)
                {
                    return FreebieErrors.NoFreebieFound();
                }

                var result = new ClientFreebieResult
                {
                    Freebies = freebies.Select(fr => new ClientFreebieResult.FreebiesCollection
                    {
                        TransactionNumber = fr.Id,
                        Status = fr.Status,
                        ESignature = fr.ESignaturePath,
                        FreebieRequestId = fr.Id,
                        Freebies = fr.FreebieItems.Select(item => new ClientFreebieResult.Items
                        {
                            Id = item.Id,
                            ItemCode = item.Items.ItemCode,
                            ItemDescription = item.Items.ItemDescription,
                            Uom = item.Items.Uom.UomCode,
                            Quantity = item.Quantity
                        })
                    }).ToList()
                };

                return Result.Success(result);

            }
        }
    }
}
