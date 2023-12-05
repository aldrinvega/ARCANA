using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Errors;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Company;

[Route("api/Company")]
[ApiController]

public class UpdateCompanyStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateCompanyStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateCompanyStatusCommand : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateCompanyStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateCompanyStatusCommand request, CancellationToken cancellationToken)
        {
            var validateCompany =
                await _context.Companies.FirstOrDefaultAsync(x => 
                x.Id == request.CompanyId, cancellationToken);

            if (validateCompany is null)
            {
                return CompanyErrors.NotFound();
            }

            validateCompany.IsActive = !validateCompany.IsActive;
            validateCompany.ModifiedBy = request.ModifiedBy;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPatch("UpdateCompanyStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id)
    {
        try
        {
            var command = new UpdateCompanyStatusCommand
            {
                CompanyId = id,
                ModifiedBy = User.Identity?.Name
            };
            var result = await _mediator.Send(command);
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
}