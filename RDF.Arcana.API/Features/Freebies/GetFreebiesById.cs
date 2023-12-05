using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Prospecting")]
[ApiController]
public class GetFreebiesById : ControllerBase
{
    private readonly IMediator _mediator;

    public GetFreebiesById(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetFreebiesById/{id}")]
    public async Task<IActionResult> GetFreebiesByIdController([FromRoute] int id)
    {
        try
        {
            var query = new GetFreebiesByIdQuery();
            query.ClientId = id;
            var result = await _mediator.Send(query);
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

    public class GetFreebiesByIdQuery : IRequest<Result>
    {
        public int ClientId { get; set; }
    }

    public class GetFreebiesByIdResult
    {
        public int? TransactionNumber { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprove { get; set; }
        public ICollection<Freebie> Freebies { get; set; }

        public class Freebie
        {
            public int Id { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetFreebiesByIdQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetFreebiesByIdQuery request,
            CancellationToken cancellationToken)
        {
            var existingFreebies = await _context.FreebieRequests
                .Include(x => x.Approvals)
                .Include(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .FirstOrDefaultAsync(x => x.ClientId == request.ClientId, cancellationToken);

            var result =  new GetFreebiesByIdResult
            {
                TransactionNumber = existingFreebies.Id,
                Status = existingFreebies.Status,
                IsActive = existingFreebies.Approvals.IsActive,
                IsApprove = existingFreebies.Approvals.IsApproved,
                Freebies = existingFreebies.FreebieItems.Select(x => new GetFreebiesByIdResult.Freebie
                {
                    Id = x.Id,
                    ItemCode = x.Items.ItemCode,
                    ItemDescription = x.Items.ItemDescription,
                    Quantity = x.Quantity
                }).ToList()
            };

            return Result.Success(result);
        }
    }
}