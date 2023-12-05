using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Errors;
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

    public class UpdateCompanyCommand : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ModifiedBy { get; set; }
    }
 
    public class Handler : IRequestHandler<UpdateCompanyCommand, Result>
    {
        private readonly ArcanaDbContext _context;
 
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }
 
        public async Task<Result> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _context.Companies.FirstOrDefaultAsync(
                x => x.Id == request.CompanyId, cancellationToken
            );
 
            if (existingCompany is null)
            {
                return CompanyErrors.NotFound();
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
            return Result.Success();
        }
    }
    
    [HttpPut]
    [Route("UpdateCompany/{id:int}")]
    public async Task<IActionResult> Update(UpdateCompany.UpdateCompanyCommand command, [FromRoute] int id)
    {
        try
        {
            command.ModifiedBy = User.Identity?.Name;
            command.CompanyId = id;
           var result =  await _mediator.Send(command);

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