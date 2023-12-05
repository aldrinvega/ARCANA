using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Items.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Items;

[Route("api/Items")]
[ApiController]

public class UpdateItemStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateItemStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateItemStatusCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateItemStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateItemStatusCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
            if (item == null)
            {
                return ItemErrors.NotFound(request.Id);
            }
        
            item.IsActive = !item.IsActive;
            item.ModifiedBy = request.ModifiedBy ?? "Admin";
            item.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPatch("UpdateItemStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute]int id)
    {
        try
        {
            var command = new UpdateItemStatusCommand
            {
                Id = id,
                ModifiedBy = User.Identity?.Name
            };
           var result = await _mediator.Send(command);
           if (result.IsFailure)
           {
               return BadRequest(result);
           }
            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }      
    }
}