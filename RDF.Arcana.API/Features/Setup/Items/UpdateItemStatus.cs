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

    public class UpdateItemStatusCommand : IRequest<Unit>
    {
        public string ItemCode { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateItemStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateItemStatusCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);
        
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
        
            item.IsActive = !item.IsActive;
            item.ModifiedBy = request.ModifiedBy ?? "Admin";
            item.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPatch("UpdateItemStatus/{itemCode}")]
    public async Task<IActionResult> UpdateStatus([FromRoute]string itemCode)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateItemStatusCommand
            {
                ItemCode = itemCode,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Successfully updated the item status");
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