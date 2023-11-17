using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Errors;

namespace RDF.Arcana.API.Features.Setup.Company;

[Route("api/Company")]
[ApiController]
public class AddNewCompany : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewCompany(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("AddNewCompany")]
    public async Task<IActionResult> AddCompany(AddNewCompanyCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    public class AddNewCompanyCommand : IRequest<Result<CompanyResult>>
    {
        public string CompanyName { get; set; }
        public int AddedBy { get; set; }
    }

    public sealed class CompanyResult
    {
        public string CompanyName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<AddNewCompanyCommand, Result<CompanyResult>>
    {
        private readonly ArcanaDbContext _context;
        private readonly IMapper _mapper;

        public Handler(ArcanaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CompanyResult>> Handle(AddNewCompanyCommand command,
            CancellationToken cancellationToken)
        {
            var existingCompany =
                await _context.Companies.FirstOrDefaultAsync(c => c.CompanyName == command.CompanyName,
                    cancellationToken);

            if (existingCompany != null)
            {
                return Result<CompanyResult>.Failure(CompanyErrors.AlreadyExist(command.CompanyName));
            }

            var company = new Domain.Company
            {
                CompanyName = command.CompanyName,
                AddedBy = command.AddedBy,
                IsActive = true
            };

            var result = new CompanyResult
            {
                CompanyName = company.CompanyName,
                IsActive = company.IsActive,
                CreatedAt = DateTime.Now
            };

            await _context.Companies.AddAsync(company, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<CompanyResult>.Success(result, "Company added successfully");
        }
    }
}