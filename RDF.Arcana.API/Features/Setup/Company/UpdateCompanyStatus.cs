using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
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

    public class UpdateCompanyStatusCommand : IRequest<Unit>
    {
        public int CompanyId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateCompanyStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCompanyStatusCommand request, CancellationToken cancellationToken)
        {
            var validateCompany =
                await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);

            if (validateCompany is null)
            {
                throw new NoCompanyFoundException();
            }

            validateCompany.IsActive = !validateCompany.IsActive;
            validateCompany.ModifiedBy = request.ModifiedBy;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPatch("UpdateCompanyStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateCompanyStatusCommand
            {
                CompanyId = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Successfully updated the status");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}