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

    public class UpdateProductSubCategoryStatusCommand : IRequest<Unit>
    {
        public int ProductSubCategoryId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateProductSubCategoryStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductSubCategoryStatusCommand request,
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
            return Unit.Value;
        }
    }
    [HttpPatch("UpdateProductSubCategoryStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute]int id)
    {
        var response = new QueryOrCommandResult<object>();
    
        try
        {
            var command = new UpdateProductSubCategoryStatusCommand {
                ProductSubCategoryId = id,
                ModifiedBy = User.Identity?.Name
            };
    
            await _mediator.Send(command);
    
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Product Sub Category status has been successfully updated");
            
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