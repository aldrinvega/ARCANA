using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Items.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Items;

[Route("api/Items")]
[ApiController]

public class UpdateItem : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateItem(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateItemCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ModifiedBy { get; set; }
        public int UomId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int MeatTypeId { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateItemCommand, Unit>
    {
        private readonly ArcanaDbContext _context;
    
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }
    
        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
         {
             var existingItem = 
                 await _context.Items.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
         
             if (existingItem.ItemCode != request.ItemCode)
             {
                 var validateItemCode =
                     await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);
                 if (validateItemCode is not null)
                 {
                     throw new Exception("Item code already exists.");
                 }
             }
         
             if (existingItem.ItemCode == request.ItemCode &&
                 existingItem.ItemDescription == request.ItemDescription &&
                 existingItem.UomId == request.UomId &&
                 existingItem.ProductSubCategoryId == request.ProductSubCategoryId &&
                 existingItem.MeatTypeId == request.MeatTypeId
             )
             {
                 throw new Exception("No changes");
             }

             existingItem.ItemCode = existingItem.ItemCode;
             existingItem.ItemDescription = request.ItemDescription;
             existingItem.UomId = request.UomId;
             existingItem.ProductSubCategoryId = request.ProductSubCategoryId;
             existingItem.MeatTypeId = request.MeatTypeId;
             existingItem.UpdatedAt = DateTime.Now;
             existingItem.ModifiedBy = request.ModifiedBy ?? "Admin";
         
             await _context.SaveChangesAsync(cancellationToken);
         
             return Unit.Value;
         }
    }
    
    [HttpPut("UpdateItem/{id:int}")]
    public async Task<IActionResult> Update(UpdateItemCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ModifiedBy = User.Identity?.Name;
            command.Id = id;
            await _mediator.Send(command);
            response.Messages.Add("ItemCode has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Ok(response);
        }
    }
    
}