using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
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

    public class UpdateProductSubCategoryCommand : IRequest<Unit>
    {
        public int ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateProductSubCategoryCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductSubCategoryCommand request, CancellationToken cancellationToken)
         {
             var existingProductSubCategory = await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.Id == request.ProductSubCategoryId, cancellationToken);
         
             if (existingProductSubCategory == null)
             {
                 throw new NoProductSubCategoryFoundException();
             }
         
             var validateProductCategory = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId, cancellationToken);
         
             if (validateProductCategory == null)
             {
                 throw new NoProductCategoryFoundException();
             }
         
             var isSubCategoryNameAlreadyExist = await _context.ProductSubCategories.AnyAsync(x => x.Id != request.ProductSubCategoryId && x.ProductSubCategoryName == request.ProductSubCategoryName, cancellationToken);
         
             if (isSubCategoryNameAlreadyExist)
             {
                 throw new ProductSubCategoryAlreadyExistException(request.ProductSubCategoryName);
             }
         
             if (existingProductSubCategory.ProductSubCategoryName == request.ProductSubCategoryName && existingProductSubCategory.ProductCategoryId == request.ProductCategoryId)
             {
                 throw new Exception("No changes");
             }
         
             existingProductSubCategory.ProductSubCategoryName = request.ProductSubCategoryName;
             existingProductSubCategory.ProductCategoryId = request.ProductCategoryId;
             existingProductSubCategory.ModifiedBy = request.ModifiedBy;
             existingProductSubCategory.UpdatedAt = DateTime.UtcNow;
         
             await _context.SaveChangesAsync(cancellationToken);
             return Unit.Value;
         }
    }
    [HttpPut("UpdateProductSubCategory/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id,
        UpdateProductSubCategoryCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ProductSubCategoryId = id;
            command.ModifiedBy = User.Identity?.Name;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Product Sub Category has been updated successfully");
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