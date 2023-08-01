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

    public class AddNewProductSubCategoryCommand : IRequest<Unit>
    {
        public string ProductSubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
        public int AddedBy { get; set; }
    }
    public class Handler : IRequestHandler<AddNewProductSubCategoryCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewProductSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingProductSubCategory =
                await _context.ProductSubCategories.FirstOrDefaultAsync(
                    x => x.ProductSubCategoryName == request.ProductSubCategoryName, cancellationToken);

            if (existingProductSubCategory != null)
            {
                throw new ProductSubCategoryAlreadyExistException(request.ProductSubCategoryName);
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
            return Unit.Value;
        }
    }
    
    [HttpPost("AddNewProductSubCategory")]
    public async Task<IActionResult> Add(
        AddNewProductSubCategoryCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Product Sub Category successfully added");
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