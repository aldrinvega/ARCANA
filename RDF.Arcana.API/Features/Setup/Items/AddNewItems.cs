using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Meat_Type;
using RDF.Arcana.API.Features.Setup.Product_Category;
using RDF.Arcana.API.Features.Setup.UOM;

namespace RDF.Arcana.API.Features.Setup.Items;

[Route("api/Items")]
[ApiController]

public class AddNewItems : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewItems(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewItemsCommand : IRequest<Result>
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int AddedBy { get; set; }
        public int UomId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int MeatTypeId { get; set; }
        public decimal Price { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewItemsCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewItemsCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _context.Items.FirstOrDefaultAsync(x => 
                    x.ItemCode == request.ItemCode, 
                cancellationToken);
            
            if (existingItem is not null)
            {
                return ItemErrors.AlreadyExist(request.ItemCode);
            }
            
            var validateProductCategory = await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.Id == request.ProductSubCategoryId, cancellationToken);
            
            if (validateProductCategory is null)
            {
                return ProductCategoryErrors.NotFound();
            }
            
            var validateUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);
            
            if (validateUom is null)
            {
                return UomErrors.NotFound();
            }
            
            var validateMeatType = await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

            if (validateMeatType is null)
            {
                return MeatTypeErrors.NotFound();
            }

            var items = new Domain.Items
            {
                ItemCode = request.ItemCode,
                ItemDescription = request.ItemDescription,
                UomId = request.UomId,
                ProductSubCategoryId = request.ProductSubCategoryId,
                AddedBy = request.AddedBy,
                MeatTypeId = request.MeatTypeId,
                IsActive = true
            };

            await _context.Items.AddAsync(items, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var newPriceChange = new ItemPriceChange
            {
                ItemId = items.Id,
                EffectivityDate = DateTime.Now,
                AddedBy = request.AddedBy,
                Price = request.Price
            };

            await _context.ItemPriceChanges.AddAsync(newPriceChange, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();

        }
    }
    
    [HttpPost("AddNewItem")]
    public async Task<IActionResult> AddNewItem(AddNewItemsCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}