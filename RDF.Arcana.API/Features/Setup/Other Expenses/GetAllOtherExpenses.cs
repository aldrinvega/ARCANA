using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Other_Expenses;
[Route("api/OtherExpenses"), ApiController]

public class GetAllOtherExpenses : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllOtherExpenses(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllOtherExpenses")]
    public async Task<IActionResult> Get([FromQuery] GetAllOtherExpensesQuery query)
    {
        try
        {
            var otherExpenses = await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                otherExpenses.CurrentPage,
                otherExpenses.PageSize,
                otherExpenses.TotalPages,
                otherExpenses.TotalCount,
                otherExpenses.HasPreviousPage,
                otherExpenses.HasNextPage
                );

            var result = new
            {
                otherExpenses,
                otherExpenses.CurrentPage,
                otherExpenses.PageSize,
                otherExpenses.TotalPages,
                otherExpenses.TotalCount,
                otherExpenses.HasPreviousPage,
                otherExpenses.HasNextPage
            };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public class GetAllOtherExpensesQuery : UserParams, IRequest<PagedList<GetAllOtherExpensesResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetAllOtherExpensesResult
    {
        public int Id { get; set; }
        public string ExpenseType { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllOtherExpensesQuery, PagedList<GetAllOtherExpensesResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllOtherExpensesResult>> Handle(GetAllOtherExpensesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<OtherExpenses> otherExpenses =  _context.OtherExpenses
                .Include(user => user.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                otherExpenses = otherExpenses.Where(oe => oe.ExpenseType.Contains(request.Search));
            }

            if (request.Status != null)
            {
                otherExpenses = otherExpenses.Where(oe => oe.IsActive == request.Status);
            }

            var result = otherExpenses.Select(oe => new GetAllOtherExpensesResult
            {
                Id = oe.Id,
                ExpenseType = oe.ExpenseType,
                CreatedAt = oe.CreatedAt.ToString("MM/dd/yyyy"),
                UpdatedAt = oe.UpdatedAt.ToString("MMM/dd/yyyy"),
                IsActive = oe.IsActive,
                AddedBy = oe.AddedByUser.Fullname
            });

            return await PagedList<GetAllOtherExpensesResult>.CreateAsync(result, request.PageNumber, request.PageSize);

        }
    }
}