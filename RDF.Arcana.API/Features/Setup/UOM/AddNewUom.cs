using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UOM;

[Route("api/Uom")]
[ApiController]

public class AddNewUom : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewUom(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewUomCommand : IRequest<Result>
    {
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public int AddedBy { get; set; }
    }
    public class Handler : IRequestHandler<AddNewUomCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewUomCommand request, CancellationToken cancellationToken)
        {
            var existingUom =
                await _context.Uoms.FirstOrDefaultAsync(x => x.UomCode == request.UomCode, cancellationToken);
            if (existingUom is not null)
            {
                return UomErrors.AlreadyExist(request.UomDescription);
            }

            var uom = new Uom
            {
                UomCode = request.UomCode,
                UomDescription = request.UomDescription,
                IsActive = true,
                AddedBy = request.AddedBy
            };
            await _context.Uoms.AddAsync(uom, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPost("AddNewUom")]
    public async Task<IActionResult> Add(AddNewUomCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            var result =  await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
}