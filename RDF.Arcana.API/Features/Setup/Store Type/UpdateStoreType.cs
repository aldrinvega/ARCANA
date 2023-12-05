using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Store_Type.Exception;

namespace RDF.Arcana.API.Features.Setup.Store_Type;

[Route("api/StoreType")]
[ApiController]

public class UpdateStoreType : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateStoreType(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateStoreTypeCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string StoreTypeName { get; set; }
        public int? ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateStoreTypeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateStoreTypeCommand request, CancellationToken cancellationToken)
        {
            var existingStoreType =
                await _context.StoreTypes.FirstOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken);

            if (existingStoreType is null)
            {
                return StoreTypeErrors.AlreadyExist(request.StoreTypeName);
            }

            existingStoreType.StoreTypeName = request.StoreTypeName;
            existingStoreType.ModifiedBy = request.ModifiedBy;
            existingStoreType.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

    [HttpPut("UpdateStoreType/{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateStoreTypeCommand command, [FromRoute] int id)
    {
        try
        {
            command.Id = id;
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.ModifiedBy = userId;
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}