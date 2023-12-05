using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Product_Category;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;
using RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

[Route("api/ProductSubCategory")]
[ApiController]

public class UpdateProductSubCategory : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProductSubCategory(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateProductSubCategoryCommand : IRequest<Result>
    {
        public int ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateProductSubCategoryCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateProductSubCategoryCommand request, CancellationToken cancellationToken)
         {
             var existingProductSubCategory = await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.Id == request.ProductSubCategoryId, cancellationToken);
         
             if (existingProductSubCategory is null)
             {
                 return ProductSubCategoryErrors.NotFound();
             }
         
             var validateProductCategory = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId, cancellationToken);
         
             if (validateProductCategory is null)
             {
                 return ProductCategoryErrors.NotFound();
             }
         
             var isSubCategoryNameAlreadyExist = await _context.ProductSubCategories.AnyAsync(x => x.Id != request.ProductSubCategoryId && x.ProductSubCategoryName == request.ProductSubCategoryName, cancellationToken);
         
             if (isSubCategoryNameAlreadyExist)
             {
                 return ProductSubCategoryErrors.AlreadyExist(request.ProductSubCategoryName);
             }
         
             existingProductSubCategory.ProductSubCategoryName = request.ProductSubCategoryName;
             existingProductSubCategory.ProductCategoryId = request.ProductCategoryId;
             existingProductSubCategory.ModifiedBy = request.ModifiedBy;
             existingProductSubCategory.UpdatedAt = DateTime.UtcNow;
         
             await _context.SaveChangesAsync(cancellationToken);
             return Result.Success();
         }
    }
    [HttpPut("UpdateProductSubCategory/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id,
        UpdateProductSubCategoryCommand command)
    {
        try
        {
            command.ProductSubCategoryId = id;
            command.ModifiedBy = User.Identity?.Name;
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