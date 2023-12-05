using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Store_Type.Exception;

namespace RDF.Arcana.API.Features.Setup.Store_Type;

[Route("api/StoreType")]
[ApiController]

public class UpdateStoreTypeStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateStoreTypeStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateStoreTypeStatusCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateStoreTypeStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateStoreTypeStatusCommand request, CancellationToken cancellationToken)
        {
            var existingStoreType =
                await _context.StoreTypes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingStoreType is null)
            {
                return StoreTypeErrors.NotFound();
            }

            existingStoreType.IsActive = !existingStoreType.IsActive;
            existingStoreType.ModifiedBy = request.ModifiedBy;
            existingStoreType.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

    [HttpPatch("UpdateStoreTypeStatus/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            var command = new UpdateStoreTypeStatusCommand();
                if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                command.ModifiedBy = userId;
                }

                command.Id = id;

                var result = await _mediator.Send(command);
                
                return Ok(result);
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }
}