using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Transactions
{
    [Route("api/clients"), ApiController]
    public class GetClientsForPOSAsync : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetClientsForPOSAsync(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("pos")]
        public async Task<IActionResult> Get([FromQuery]GetClientsForPOSAsyncQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                if (result.IsFailure)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        public record GetClientsForPOSAsyncQuery : IRequest<Result>
        {
            public string Search { get; set; }
        }

        public class GetClientsForPOSAsyncResult
        {
            public int ClientId { get; set; }
            public string OwnersName { get; set; }
            public string BusinessName { get; set; }
        }

        public class Handler : IRequestHandler<GetClientsForPOSAsyncQuery, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetClientsForPOSAsyncQuery request, CancellationToken cancellationToken)
            {
                var clients = await _context.Clients.Select(cl => new GetClientsForPOSAsyncResult
                {
                    ClientId = cl.Id,
                    BusinessName = cl.BusinessName,
                    OwnersName = cl.Fullname
                }).ToListAsync(cancellationToken: cancellationToken);

                if (request.Search is not null)
                {
                    clients = clients.Where(cl => 
                    cl.OwnersName.Contains(request.Search) || 
                    cl.BusinessName.Contains(request.Search)).ToList();
                }

                return Result.Success(clients);
                
            }
        }
    }
}
