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

    public class UpdateProductCategoryStatusCommand : IRequest<Unit>
    {
        public int ProductCategoryId { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateProductCategoryStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCategoryStatusCommand request, CancellationToken cancellationToken)
        {
            var existingProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId,
                    cancellationToken);
            if (existingProductCategory is null)
            {
                throw new NoProductCategoryFoundException();
            }

            existingProductCategory.IsActive = !existingProductCategory.IsActive;
            existingProductCategory.UpdatedAt = DateTime.Now;
            existingProductCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPatch("UpdateProductCategoryStatus/{productCategoryId:int}")]
    public async Task<IActionResult> Update([FromRoute] int productCategoryId)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateProductCategoryStatusCommand
            {
                ProductCategoryId = productCategoryId,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Product Category status has been updated");
            response.Status = StatusCodes.Status200OK;
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