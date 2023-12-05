using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

[Route("api/ProductSubCategory")]
[ApiController]

public class AddNewProductSubCategory : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewProductSubCategory(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewProductSubCategoryCommand : IRequest<Result>
    {
        public string ProductSubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
        public int AddedBy { get; set; }
    }
    public class Handler : IRequestHandler<AddNewProductSubCategoryCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewProductSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingProductSubCategory =
                await _context.ProductSubCategories.FirstOrDefaultAsync(
                    x => x.ProductSubCategoryName == request.ProductSubCategoryName, cancellationToken);

            if (existingProductSubCategory != null)
            {
                return ProductSubCategoryErrors.AlreadyExist(request.ProductSubCategoryName);
            }

            var productSubCategory = new ProductSubCategory
            {
                ProductSubCategoryName = request.ProductSubCategoryName,
                ProductCategoryId = request.ProductCategoryId,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.ProductSubCategories.AddAsync(productSubCategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPost("AddNewProductSubCategory")]
    public async Task<IActionResult> Add(
        AddNewProductSubCategoryCommand command)
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
            return Conflict(e.Message);
        }
    }
}