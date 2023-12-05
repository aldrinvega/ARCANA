using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

[Route("api/MeatType")]
[ApiController]

public class UpdateMeatTypeStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateMeatTypeStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateMeatTypeStatusCommand : IRequest<Result>
    {
        public int MeatTypeId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateMeatTypeStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateMeatTypeStatusCommand request, CancellationToken cancellationToken)
        {
            var existingMeatType = await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

            if (existingMeatType is null)
            {
                return MeatTypeErrors.NotFound();
            }

            existingMeatType.IsActive = !existingMeatType.IsActive;
            existingMeatType.UpdatedAt = DateTime.Now;
            existingMeatType.ModifiedBy = request.ModifiedBy ?? Roles.Admin;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPatch("UpdateMeatTypeStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id)
    {
        try
        {
            var command = new UpdateMeatTypeStatusCommand
            {
                MeatTypeId = id,
                ModifiedBy = User.Identity?.Name
            };
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}