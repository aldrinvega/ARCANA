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

    public class UpdateStoreTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string StoreTypeName { get; set; }
        public int? ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateStoreTypeCommand, Unit>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStoreTypeCommand request, CancellationToken cancellationToken)
        {
            var existingStoreType =
                await _context.StoreTypes.FirstOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken);
            if (existingStoreType.StoreTypeName == request.StoreTypeName)
            {
                throw new System.Exception("No changes");
            }

            if (existingStoreType is null)
            {
                throw new StoreTypeNotFoundException();
            }

            existingStoreType.StoreTypeName = request.StoreTypeName;
            existingStoreType.ModifiedBy = request.ModifiedBy;
            existingStoreType.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    [HttpPut("UpdateStoreType/{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateStoreTypeCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.Id = id;
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.ModifiedBy = userId;
            }

            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Store type is updated successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}