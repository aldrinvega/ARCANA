using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Company;

[Route("api/Company")]
[ApiController]

public class GetCompaniesAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetCompaniesAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetCompaniesQuery : UserParams, IRequest<PagedList<GetCompaniesResult>>
    {
        public bool? Status { get; set; }
        public string Search { get; set; }
    }

    public class GetCompaniesResult
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string AddedBy { get; set; }
        public IEnumerable<string> Users { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
     public class Handler : IRequestHandler<GetCompaniesQuery, PagedList<GetCompaniesResult>>
     {
         private readonly DataContext _context;
         
         public Handler(DataContext context)
         {
             _context = context;
         }
         
        public async Task<PagedList<GetCompaniesResult>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
           {
               var companies = _context.Companies
                   .Include(x => x.Users)
                   .Include(x => x.AddedByUser)
                   .AsQueryable();
           
               
               if (!string.IsNullOrEmpty(request.Search))
               {
                   companies = companies.Where(x => x.CompanyName.Contains(request.Search));
               }
           
               if (request.Status != null)
               {
                   companies = companies.Where(x => x.IsActive == request.Status);
               }
           
               var result = companies.Select(x => x.ToGetAllCompaniesResult());
               
               return await PagedList<GetCompaniesResult>.CreateAsync(result, request.PageNumber, request.PageSize);
           }
     }
     
     [HttpGet("GetAllCompanies")]
     public async Task<IActionResult> GetAllCompanies([FromQuery]GetCompaniesQuery request)
     {
         var response = new QueryOrCommandResult<object>();
      
         try
         {
             var companies = await _mediator.Send(request);
             Response.AddPaginationHeader(
                 companies.CurrentPage,
                 companies.PageSize,
                 companies.TotalCount,
                 companies.TotalPages,
                 companies.HasPreviousPage,
                 companies.HasNextPage
             );
             var results = new QueryOrCommandResult<object>
             {
                 Success = true,
                 Data = new
                 {
                     companies,
                     companies.CurrentPage,
                     companies.PageSize,
                     companies.TotalCount,
                     companies.TotalPages,
                     companies.HasPreviousPage,
                     companies.HasNextPage
                 },
                 Status = StatusCodes.Status200OK
             };
             results.Messages.Add("Successfully Fetch");
             return Ok(results);
         }
         catch (Exception e)
         {
             response.Status = StatusCodes.Status409Conflict;
             return Conflict(e.Message);
         }
     }
}