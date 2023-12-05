using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Requests_Approval;
[Route("api/Approvers"), ApiController]

public class GetAllApprovers : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllApprovers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllApprovers")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllApproversQuery();

        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllApproversQuery : IRequest<Result> {}

    public class GetAllApproversResult
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllApproversQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetAllApproversQuery request, CancellationToken cancellationToken)
        {
            var approvers = await _context.Users
                .Include(ur => ur.UserRoles)
                .Where(rn => rn.UserRoles.UserRoleName == Roles.Approver)
                .ToListAsync(cancellationToken);

            var result = approvers.Select(x => new GetAllApproversResult
            {
                UserId = x.Id,
                Fullname = x.Fullname
            });

            return Result.Success(result);
        }
    }
}