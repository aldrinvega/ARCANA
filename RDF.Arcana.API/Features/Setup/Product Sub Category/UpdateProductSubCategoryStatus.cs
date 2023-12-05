using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

[Route("api/ProductSubCategory")]
[ApiController]

public class UpdateProductSubCategoryStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProductSubCategoryStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateProductSubCategoryStatusCommand : IRequest<Result>
    {
        public int ProductSubCategoryId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateProductSubCategoryStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateProductSubCategoryStatusCommand request,
            CancellationToken cancellationToken)
        {
            var existingProductSubCategory =
                await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.Id == request.ProductSubCategoryId,
                    cancellationToken);
            if (existingProductSubCategory == null)
            {
                throw new NoProductSubCategoryFoundException();
            }

            existingProductSubCategory.IsActive = !existingProductSubCategory.IsActive;
            existingProductSubCategory.UpdatedAt = DateTime.Now;
            existingProductSubCategory.ModifiedBy = request.ModifiedBy;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    [HttpPatch("UpdateProductSubCategoryStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute]int id)
    {
        try
        {
            var command = new UpdateProductSubCategoryStatusCommand {
                ProductSubCategoryId = id,
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