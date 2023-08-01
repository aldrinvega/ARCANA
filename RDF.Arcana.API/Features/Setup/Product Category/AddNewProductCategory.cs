using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

[Route("api/ProductCategory")]
[ApiController]

public class AddNewProductCategory : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewProductCategory(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewProductCategoryCommand : IRequest<Unit>
    {
        public string ProductCategoryName { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewProductCategoryCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(
                    x => x.ProductCategoryName == request.ProductCategoryName, cancellationToken);

            if (existingProductCategory is not null)
            {
                throw new ProductCategoryAlreadyExistException();
            }

            var productCategory = new ProductCategory
            {
                ProductCategoryName = request.ProductCategoryName,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.ProductCategories.AddAsync(productCategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPost("AddNewProductCategory")]
    public async Task<IActionResult> Add(AddNewProductCategoryCommand command)
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
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Product Category successfully added");
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