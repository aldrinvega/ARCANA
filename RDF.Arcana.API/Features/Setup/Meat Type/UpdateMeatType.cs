using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

[Route("api/MeatType")]
[ApiController]

public class UpdateMeatType : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateMeatType(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateMeatTypeCommand : IRequest<Result>
    {
        public int MeatTypeId { get; set; }
        public string MeatTypeName { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateMeatTypeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateMeatTypeCommand request, CancellationToken cancellationToken)
        {
            var existingMeatType = await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

            var validateMeatTypeName = await _context.MeatTypes.Where(x => x.MeatTypeName == request.MeatTypeName).FirstOrDefaultAsync(cancellationToken);

            if (validateMeatTypeName is null)
            {
                existingMeatType.MeatTypeName = request.MeatTypeName;
                existingMeatType.ModifiedBy = request.ModifiedBy ?? Roles.Admin;
                existingMeatType.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            
            if (validateMeatTypeName.MeatTypeName == request.MeatTypeName && validateMeatTypeName.Id != request.MeatTypeId)
            {
                return MeatTypeErrors.AlreadyExist(request.MeatTypeName);
            }
            
            return Result.Success();
        }
    }
    
    [HttpPut("UpdateMeatType/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateMeatTypeCommand command)
    {
        try
        {
            command.ModifiedBy = User.Identity?.Name;
            command.MeatTypeId = id;
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }
}