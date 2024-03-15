using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using Result = RDF.Arcana.API.Common.Result;

namespace RDF.Arcana.API.Features.Expenses;

[Route("api/Expenses"), ApiController]

public class GetAllExpenses : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllExpenses(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllExpenses")]
    public async Task<IActionResult> Get([FromQuery] GetAllExpensesQuery query)
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
            
            var expenses = await _mediator.Send(query);
            Response.AddPaginationHeader(
                expenses.CurrentPage,
                expenses.PageSize,
                expenses.TotalCount,
                expenses.TotalPages,
                expenses.HasPreviousPage,
                expenses.HasNextPage
                );

            var result = new
            {
                expenses,
                expenses.CurrentPage,
                expenses.PageSize,
                expenses.TotalCount,
                expenses.TotalPages,
                expenses.HasPreviousPage,
                expenses.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllExpensesQuery : UserParams, IRequest<PagedList<GetAllExpensesResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public string RoleName { get; set; }
        public int AccessBy { get; set; }
        public string ExpenseStatus { get; set; }
    }

    public class GetAllExpensesResult
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ClientId { get; set; }
        public string OwnersName { get; set; }
        public string BusinessName { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<ExpensesRequestCollection> Expenses { get; set; }
        public class ExpensesRequestCollection
        {
            public int Id { get; set; }
            public string ExpenseType { get; set; }
            public decimal Amount { get; set; }
        }
        public IEnumerable<ExpensesApprovalHistory> ApprovalHistories { get; set; }
        public IEnumerable<UpdateHistory> UpdateHistories { get; set; }
        public string Requestor { get; set; }
        public string RequestorMobileNumber { get; set; }
        public string CurrentApprover { get; set; }
        public string CurrentApproverPhoneNumber { get; set; }
        public string NextApprover { get; set; }
        public string NextApproverPhoneNumber { get; set; }
        public class ExpensesApprovalHistory
        {
            public string Module { get; set; }
            public string Approver { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Status { get; set; }
            public int? Level { get; set; }
            public string Reason { get; set; }
        }
        public class UpdateHistory
        {
            public string Module { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
    
    public class Handler : IRequestHandler<GetAllExpensesQuery, PagedList<GetAllExpensesResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllExpensesResult>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {

            IQueryable<Domain.Expenses> expenses = _context.Expenses
            .Include(er => er.ExpensesRequests)
            .ThenInclude(oe => oe.OtherExpense)
            .Include(rq => rq.Request)
            .Include(rq => rq.AddedByUser);

            var userClusters = await _context.CdoClusters.FirstOrDefaultAsync(x => x.UserId == request.AccessBy, cancellationToken);



            if (!string.IsNullOrEmpty(request.Search))
            {
                expenses = expenses.Where(oe => oe.Client.BusinessName.Contains(request.Search));
            }

            switch (request.RoleName)
            {
                case var roleName when roleName.Contains(Roles.Approver) && 
                !string.IsNullOrWhiteSpace(request.ExpenseStatus) &&
                             request.ExpenseStatus.ToLower() != Status.UnderReview.ToLower():
                    expenses = expenses.Where(lf => lf.Request.Approvals.Any(x =>
                        x.Status == request.ExpenseStatus && x.ApproverId == request.AccessBy && x.IsActive) && lf.Client.RegistrationStatus != Status.UnderReview);
                    break;

                case var roleName when roleName.Contains(Roles.Approver):
                    expenses = expenses.Where(lf =>
                        lf.Request.Status == request.ExpenseStatus && lf.Request.CurrentApproverId == request.AccessBy && lf.Client.RegistrationStatus != Status.UnderReview);
                    break;

                case Roles.Cdo:
                    expenses = expenses.Where(x => x.Status == request.ExpenseStatus && x.Client.ClusterId == userClusters.ClusterId && x.Client.RegistrationStatus != Status.UnderReview);
                    break;

                case Roles.Admin:
                    expenses = expenses.Where(x => x.Status == request.ExpenseStatus && x.Client.RegistrationStatus != Status.UnderReview);
                    break;

                default:
                    // No additional filtering for other roles
                    break;
            }



            if (request.RoleName.Contains(Roles.Approver) && request.ExpenseStatus == Status.UnderReview)
            {
                expenses = expenses.Where(x => x.Request.CurrentApproverId == request.AccessBy && x.Client.RegistrationStatus != Status.UnderReview);
            }

            if (request.Status != null)
            {
                expenses = expenses.Where(x => x.IsActive == request.Status);
            }
            
            if (request.Status != null)
            {
                expenses = expenses.Where(x => x.IsActive == request.Status);
            }

            var result = expenses.Select(oe => new GetAllExpensesResult
            {
                Id = oe.Id,
                RequestId = oe.RequestId,
                ClientId = oe.Client.Id,
                BusinessName = oe.Client.BusinessName,
                OwnersName = oe.Client.Fullname,
                CreatedAt = oe.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                UpdatedAt = oe.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                Expenses = oe.ExpensesRequests.Select(er => new GetAllExpensesResult.ExpensesRequestCollection
                {
                    Id = er.Id,
                    ExpenseType = er.OtherExpense.ExpenseType,
                    Amount = er.Amount
                }),
                TotalAmount = oe.ExpensesRequests.Sum(er => er.Amount),
                Requestor = oe.AddedByUser.Fullname,
                RequestorMobileNumber = oe.AddedByUser.MobileNumber,
                CurrentApprover = oe.Request.CurrentApprover.Fullname,
                CurrentApproverPhoneNumber = oe.Request.CurrentApprover.MobileNumber,
                NextApprover = oe.Request.NextApprover.Fullname,
                NextApproverPhoneNumber = oe.Request.NextApprover.MobileNumber
            });

            return await PagedList<GetAllExpensesResult>.CreateAsync(result, request.PageNumber, request.PageSize);

        }
    }
}