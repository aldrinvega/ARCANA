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

    public class UpdateProductCategoryCommand : IRequest<Result>
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
    public class Handler : IRequestHandler<UpdateProductCategoryCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
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
                return Result.Success();
            }
            
            if (validateCategoryName.ProductCategoryName == request.ProductCategoryName && validateCategoryName.Id == request.ProductCategoryId)
            {
                throw new Exception("No changes");
            }
            
            if (validateCategoryName.ProductCategoryName == request.ProductCategoryName && validateCategoryName.Id != request.ProductCategoryId)
            {
                throw new ProductCategoryAlreadyExistException();
            }
            
            return Result.Success();
        }
    }
    
    [HttpPut("UpdateProductCategory/{id:int}")]
    public async Task<IActionResult> Update([FromBody]UpdateProductCategoryCommand command, [FromRoute]int id)
    {
        try
        {
            command.ProductCategoryId = id;
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