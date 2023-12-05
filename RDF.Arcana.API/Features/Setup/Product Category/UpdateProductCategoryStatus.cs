using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

[Route("api/ProductCategory")]
[ApiController]

public class UpdateProductCategoryStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProductCategoryStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateProductCategoryStatusCommand : IRequest<Result>
    {
        public int ProductCategoryId { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateProductCategoryStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateProductCategoryStatusCommand request, CancellationToken cancellationToken)
        {
            var existingProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId,
                    cancellationToken);
            if (existingProductCategory is null)
            {
                return ProductCategoryErrors.NotFound();
            }

            existingProductCategory.IsActive = !existingProductCategory.IsActive;
            existingProductCategory.UpdatedAt = DateTime.Now;
            existingProductCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPatch("UpdateProductCategoryStatus/{productCategoryId:int}")]
    public async Task<IActionResult> Update([FromRoute] int productCategoryId)
    {
        try
        {
            var command = new UpdateProductCategoryStatusCommand
            {
                ProductCategoryId = productCategoryId,
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