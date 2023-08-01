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

    public class AddNewUomCommand : IRequest<Unit>
    {
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public int AddedBy { get; set; }
    }
    public class Handler : IRequestHandler<AddNewUomCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewUomCommand request, CancellationToken cancellationToken)
        {
            var existingUom =
                await _context.Uoms.FirstOrDefaultAsync(x => x.UomCode == request.UomCode, cancellationToken);
            if (existingUom is not null)
            {
                throw new UomAlreadyExistException();
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
            return Unit.Value;
        }
    }
    
    [HttpPost("AddNewUom")]
    public async Task<IActionResult> Add(AddNewUomCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("UOM has been added successfully");
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