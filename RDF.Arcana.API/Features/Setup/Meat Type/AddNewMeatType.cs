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

    public class AddNewMeatTypeCommand : IRequest<Result>
    {
        public string MeatTypeName { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewMeatTypeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewMeatTypeCommand request, CancellationToken cancellationToken)
        {
            var existingMeatType = await _context.MeatTypes.FirstOrDefaultAsync(x => 
                x.MeatTypeName == request.MeatTypeName,
                cancellationToken);

            if (existingMeatType is not null)
            {
                return MeatTypeErrors.AlreadyExist(request.MeatTypeName);
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
            
            return Result.Success();
        }
    }
    
    [HttpPost("AddNewMeatType")]
    public async Task<IActionResult> Add(AddNewMeatTypeCommand command)
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
            return BadRequest(e.Message);
        }
    }
}