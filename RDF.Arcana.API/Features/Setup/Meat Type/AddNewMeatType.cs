using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

[Route("api/MeatType")]
[ApiController]

public class AddNewMeatType : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewMeatType(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewMeatTypeCommand : IRequest<Unit>
    {
        public string MeatTypeName { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewMeatTypeCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewMeatTypeCommand request, CancellationToken cancellationToken)
        {
            var existingMeatType =
                await _context.MeatTypes.FirstOrDefaultAsync(x => x.MeatTypeName == request.MeatTypeName,cancellationToken);

            if (existingMeatType is not null)
            {
                throw new MeatTypeIsAlreadyExistException(request.MeatTypeName);
            }

            var meatType = new MeatType
            {
                MeatTypeName = request.MeatTypeName,
                UpdatedAt = DateTime.Now,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.MeatTypes.AddAsync(meatType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
    
    [HttpPost("AddNewMeatType")]
    public async Task<IActionResult> Add(AddNewMeatTypeCommand command)
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
            response.Messages.Add("Meat Type successfully added");
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