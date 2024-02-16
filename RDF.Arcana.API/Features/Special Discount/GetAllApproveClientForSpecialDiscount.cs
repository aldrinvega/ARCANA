using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;
using static RDF.Arcana.API.Features.Sales_Transactions.GetClientsForPOSAsync;

namespace RDF.Arcana.API.Features.Special_Discount;


[Route("api/clients"), ApiController]
public class GetAllApproveClientForSpecialDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllApproveClientForSpecialDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("special-discount")]
    public async Task<IActionResult> Get([FromQuery] GetAllApproveClientForSpecialDiscountQuery query)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
               && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;

                var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                if (roleClaim != null)
                {
                    query.RoleName = roleClaim.Value;
                }
            }
            var result = await _mediator.Send(query);
            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public record GetAllApproveClientForSpecialDiscountQuery : IRequest<Result>
    {
        public string Search { get; set; }
        public int AccessBy { get; set; }
        public string RoleName { get; set; }
    }

    public class GetAllApproveClientForSpecialDiscountResult
    {
        public int ClientId { get; set; }
        public string OwnersName { get; set; }
        public string BusinessName { get; set; }
    }

    public class Handler : IRequestHandler<GetAllApproveClientForSpecialDiscountQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetAllApproveClientForSpecialDiscountQuery request, CancellationToken cancellationToken)
        {
            var userClusters = await _context.CdoClusters.FirstOrDefaultAsync(x => x.UserId == request.AccessBy, cancellationToken);

            List<GetAllApproveClientForSpecialDiscountResult> clients = new();

            if (userClusters != null)
            {
                clients = await _context.Clients
                    .Where(x => x.RegistrationStatus == Status.Approved && x.ClusterId == userClusters.ClusterId)
                    .Select(cl => new GetAllApproveClientForSpecialDiscountResult
                    {
                        ClientId = cl.Id,
                        BusinessName = cl.BusinessName,
                        OwnersName = cl.Fullname
                    })
                    .ToListAsync(cancellationToken: cancellationToken);

                if (request.Search is not null)
                {
                    clients = clients.Where(cl =>
                        cl.OwnersName.Contains(request.Search) ||
                        cl.BusinessName.Contains(request.Search)).ToList();
                }
            }

            if (request.RoleName.Contains(Roles.Admin))
            {
                clients = await _context.Clients
                    .Where(x => x.RegistrationStatus == Status.Approved)
                    .Select(cl => new GetAllApproveClientForSpecialDiscountResult
                    {
                        ClientId = cl.Id,
                        BusinessName = cl.BusinessName,
                        OwnersName = cl.Fullname
                    })
                    .ToListAsync(cancellationToken: cancellationToken);

                if (request.Search is not null)
                {
                    clients = clients.Where(cl =>
                        cl.OwnersName.Contains(request.Search) ||
                        cl.BusinessName.Contains(request.Search)).ToList();
                }
            }
            return Result.Success(clients);

        }
    }
}

