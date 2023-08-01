using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

[Route("api/ProductCategory")]
[ApiController]

public class UpdateProductCategory : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProductCategory(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateProductCategoryCommand : IRequest<Unit>
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
    public class Handler : IRequestHandler<UpdateProductCategoryCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId,
                    cancellationToken);

            var validateCategoryName =
                await _context.ProductCategories.Where(x => x.ProductCategoryName == request.ProductCategoryName).FirstOrDefaultAsync(cancellationToken);

            if (validateCategoryName is null)
            {
                existingProductCategory.ProductCategoryName = request.ProductCategoryName;
                existingProductCategory.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;

            }
            
            if (validateCategoryName.ProductCategoryName == request.ProductCategoryName && validateCategoryName.Id == request.ProductCategoryId)
            {
                throw new Exception("No changes");
            }
            
            if (validateCategoryName.ProductCategoryName == request.ProductCategoryName && validateCategoryName.Id != request.ProductCategoryId)
            {
                throw new ProductCategoryAlreadyExistException();
            }
            
            return Unit.Value;
        }
    }
    
    [HttpPut("UpdateProductCategory")]
    public async Task<IActionResult> Update(UpdateProductCategoryCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Product Category has been updated successfully");
            response.Success = true;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;

            return Conflict(response);
        }
    }
}