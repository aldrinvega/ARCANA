using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Items.Exceptions;
using RDF.Arcana.API.Features.Setup.Meat_Type;
using RDF.Arcana.API.Features.Setup.Price_Change;

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

    public class UpdateItemCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ModifiedBy { get; set; }
        public int UomId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int MeatTypeId { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectivityDate { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateItemCommand, Result>
    {
        private readonly ArcanaDbContext _context;
    
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }
    
        public async Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
         {
             var existingItem = 
                 await _context.Items
                     .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
             var validateMeatType =
                 await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

             if (validateMeatType is null )
             {
                 return MeatTypeErrors.NotFound();
             }
         
             if (existingItem.ItemCode != request.ItemCode)
             {
                 var validateItemCode =
                     await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);
                 if (validateItemCode is not null)
                 {
                     return ItemErrors.NotFound(request.Id);
                 }
             }
             
             existingItem.ItemCode = request.ItemCode;
             existingItem.ItemDescription = request.ItemDescription;
             existingItem.UomId = request.UomId;
             existingItem.ProductSubCategoryId = request.ProductSubCategoryId;
             existingItem.MeatTypeId = request.MeatTypeId;
             existingItem.UpdatedAt = DateTime.Now;
             existingItem.ModifiedBy = request.ModifiedBy ?? Roles.Admin;
         
             await _context.SaveChangesAsync(cancellationToken);

             return Result.Success();
         }
    }

    [HttpPut("UpdateItem/{id:int}")]
    public async Task<IActionResult> Update(UpdateItemCommand command, [FromRoute] int id)
    {
        try
        {
            command.ModifiedBy = User.Identity?.Name;
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return Ok(e.Message);
        }
    }

}