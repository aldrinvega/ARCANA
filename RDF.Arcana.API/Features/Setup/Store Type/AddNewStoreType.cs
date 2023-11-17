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
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            await _mediator.Send(command);
            response.Messages.Add($"{command.StoreTypeName} added successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    public class AddNewStoreTypeCommand : IRequest<Unit>
    {
        public string StoreTypeName { get; set; }
        public int? AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewStoreTypeCommand, Unit>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewStoreTypeCommand request, CancellationToken cancellationToken)
        {
            var existingStoreType =
                await _context.StoreTypes.FirstOrDefaultAsync(x => x.StoreTypeName == request.StoreTypeName,
                    cancellationToken);

            if (existingStoreType is not null)
            {
                throw new StoreTypeAlreadyExistException(request.StoreTypeName);
            }

            var storeType = new StoreType
            {
                StoreTypeName = request.StoreTypeName,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.StoreTypes.AddAsync(storeType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}