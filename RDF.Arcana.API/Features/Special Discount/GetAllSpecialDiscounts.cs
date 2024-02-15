using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Special_Discount;
[Route("api/special-discount")]

public class GetAllSpecialDiscounts : ControllerBase
{
    private readonly IMediator _mediator;
    public GetAllSpecialDiscounts(IMediator mediator) { _mediator = mediator; }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllSpecialDiscountQuery query)
    {
        try
        {
            
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AddedBy = userId;

                var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                if (roleClaim != null)
                {
                    query.RoleName = roleClaim.Value;
                }
            }

            var specialDiscounts = await _mediator.Send(query);

            Response.AddPaginationHeader(
                specialDiscounts.CurrentPage,
                specialDiscounts.PageSize,
                specialDiscounts.TotalCount,
                specialDiscounts.TotalPages,
                specialDiscounts.HasPreviousPage,
                specialDiscounts.HasNextPage
            );

            var result = new
            {
                specialDiscounts,
                specialDiscounts.CurrentPage,
                specialDiscounts.PageSize,
                specialDiscounts.TotalCount,
                specialDiscounts.TotalPages,
                specialDiscounts.HasPreviousPage,
                specialDiscounts.HasNextPage
            };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
 
    public class GetAllSpecialDiscountQuery : UserParams, IRequest<PagedList<SpecialDiscountResult>>
    {
        public string Search { get; set; }
        public string SpDiscountStatus { get; set; }
        public string RoleName { get; set; }
        public int AddedBy { get; set; }
        public bool? Status { get; set; }
    }

    public class SpecialDiscountResult
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RequestId { get; set; }
        public string ClientName { get; set; }
        public decimal Discount { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllSpecialDiscountQuery, PagedList<SpecialDiscountResult>>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context) { _context = context; }

        public async Task<PagedList<SpecialDiscountResult>> Handle(GetAllSpecialDiscountQuery request, CancellationToken cancellationToken)
        {
            IQueryable<SpecialDiscount> specialDiscounts = _context.SpecialDiscounts
                .Include(r => r.Request)
                .Include(cl => cl.Client);
            
            if (!string.IsNullOrEmpty(request.Search))
            {
                specialDiscounts = specialDiscounts.Where(x =>
                    x.Client.Fullname.Contains(request.Search));
            }

            switch (request.RoleName)
            {
                case var roleName when roleName.Contains(Roles.Approver) &&
                                       !string.IsNullOrWhiteSpace(request.SpDiscountStatus) &&
                                       !string.Equals(request.SpDiscountStatus, Status.UnderReview, StringComparison.CurrentCultureIgnoreCase):
                    specialDiscounts = specialDiscounts.Where(lf => lf.Request.Approvals.Any(x =>
                        x.Status == request.SpDiscountStatus && x.ApproverId == request.AddedBy && x.IsActive));
                    break;

                case var roleName when roleName.Contains(Roles.Approver):
                    specialDiscounts = specialDiscounts.Where(lf =>
                        lf.Request.Status == request.SpDiscountStatus && lf.Request.CurrentApproverId == request.AddedBy);
                    break;

                case Roles.Cdo:

                    var userClusters = await _context.CdoClusters.FirstOrDefaultAsync(x => x.UserId == request.AddedBy, cancellationToken);

                        if (request.SpDiscountStatus is Status.ForReleasing or Status.Released or Status.Voided)
                        {

                            specialDiscounts = specialDiscounts
                                .Where(x => x.AddedBy == request.AddedBy &&
                                            x.Status == request.SpDiscountStatus);

                            //Get the result

                            var voidedResults = specialDiscounts.Select(client => new SpecialDiscountResult
                            {
                                Id = client.Id,
                                ClientId = client.ClientId,
                                ClientName = client.Client.Fullname,
                                Discount = client.Discount,
                                RequestId = client.RequestId,
                                UpdatedAt = client.UpdatedAt.ToString("MM/dd/yyyy"),
                                CreatedAt = client.CreatedAt.ToString("MM/dd/yyyy")
                            });

                            voidedResults = voidedResults.OrderBy(r => r.Id);

                            //Return the result
                            return await PagedList<SpecialDiscountResult>.CreateAsync(voidedResults, request.PageNumber, request.PageSize);

                        }
                        
                        if (userClusters is null)
                        {
                            specialDiscounts = specialDiscounts
                            .Where(x => x.Status == request.SpDiscountStatus);
                            break;
                        }

                    specialDiscounts = specialDiscounts.Where(x => 
                        x.Status == request.SpDiscountStatus && 
                        x.Client.ClusterId == userClusters.ClusterId);
                    break;

                case Roles.Admin:
                    specialDiscounts = specialDiscounts.Where(x => x.Status == request.SpDiscountStatus);
                    break;
            }
            
            //Get all the under review request for the Approver
            //It will access the request table where status is Under Review
            //And CurrentApproverId is the Logged In user (Approver)
            if (request.RoleName.Contains(Roles.Approver) && request.SpDiscountStatus == Status.UnderReview)
            {
                specialDiscounts = specialDiscounts.Where(x => x.Request.CurrentApproverId == request.AddedBy);
            }
            
            //Filter based on Status
            if (request.Status != null)
            {
                specialDiscounts = specialDiscounts.Where(x => x.IsActive == request.Status);
            }
            
            var result = specialDiscounts.Select(client => new SpecialDiscountResult
            {
                Id = client.Id,
                ClientId = client.ClientId,
                ClientName = client.Client.Fullname,
                Discount = client.Discount,
                RequestId = client.RequestId,
                UpdatedAt = client.UpdatedAt.ToString("MM/dd/yyyy"),
                CreatedAt = client.CreatedAt.ToString("MM/dd/yyyy")
            });
            
            //Return the result
            return await PagedList<SpecialDiscountResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}
