using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Store_Type.Exception;

namespace RDF.Arcana.API.Features.Setup.Store_Type;

[Route("api/StoreType")]
[ApiController]
public class AddNewStoreType : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewStoreType(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewStoreType")]
    public async Task<IActionResult> Add([FromBody] AddNewStoreTypeCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
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
            return Conflict(e.Message);
        }
    }

    public class AddNewStoreTypeCommand : IRequest<Result>
    {
        public string StoreTypeName { get; set; }
        public int? AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewStoreTypeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewStoreTypeCommand request, CancellationToken cancellationToken)
        {
            var existingStoreType =
                await _context.StoreTypes.FirstOrDefaultAsync(x => x.StoreTypeName == request.StoreTypeName,
                    cancellationToken);

            if (existingStoreType is not null)
            {
                return StoreTypeErrors.AlreadyExist(request.StoreTypeName);
            }

            var storeType = new StoreType
            {
                StoreTypeName = request.StoreTypeName,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.StoreTypes.AddAsync(storeType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}