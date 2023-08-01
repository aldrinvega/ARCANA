using System.Reflection.Metadata;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;
using RDF.Arcana.API.Features.Users;

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


    public class AddNewCompanyCommand : IRequest<Unit>
    {
        public string CompanyName { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewCompanyCommand, Unit>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddNewCompanyCommand command, CancellationToken cancellationToken)
        {
            var existingCompany =
                await _context.Companies.FirstOrDefaultAsync(c => c.CompanyName == command.CompanyName,
                    cancellationToken);

            if (existingCompany != null)
            {
                throw new NoCompanyFoundException();
            }

            var company = new Domain.Company
            {
                CompanyName = command.CompanyName,
                AddedBy = command.AddedBy,
                IsActive = true
            };

            await _context.Companies.AddAsync(company, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
    [HttpPost]
    [Route("AddNewCompany")]
    public async Task<IActionResult> AddCompany(AddNewCompanyCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add($"{command.CompanyName} added successfully");
            return Ok(response);
                
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}