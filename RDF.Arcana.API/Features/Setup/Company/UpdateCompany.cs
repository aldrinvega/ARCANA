using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Company;

[Route("api/Company")]
[ApiController]

public class UpdateCompany : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateCompany(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateCompanyCommand : IRequest<Unit>
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ModifiedBy { get; set; }
    }
 
    public class Handler : IRequestHandler<UpdateCompanyCommand, Unit>
    {
        private readonly DataContext _context;
 
        public Handler(DataContext context)
        {
            _context = context;
        }
 
        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _context.Companies.FirstOrDefaultAsync(
                x => x.Id == request.CompanyId, cancellationToken
            );
 
            if (existingCompany is null)
            {
                throw new NoCompanyFoundException();
            }
 
            if (existingCompany.CompanyName == request.CompanyName)
            {
                throw new Exception("No changes");
            }
 
            var isCompanyAlreadyExist = await _context.Companies
                .AnyAsync(x => x.Id != request.CompanyId && x.CompanyName == request.CompanyName, cancellationToken);
 
            if (isCompanyAlreadyExist)
            {
                throw new CompanyAlreadyExists(request.CompanyName);
            }
 
            existingCompany.CompanyName = request.CompanyName;
            existingCompany.ModifiedBy = request.ModifiedBy;
            existingCompany.UpdatedAt = DateTime.UtcNow;
 
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPut]
    [Route("UpdateCompany/{id:int}")]
    public async Task<IActionResult> Update(UpdateCompany.UpdateCompanyCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ModifiedBy = User.Identity?.Name;
            command.CompanyId = id;
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Company successfully updated");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Ok(response);
        }
    }
}