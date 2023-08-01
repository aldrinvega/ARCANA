using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Items.Exceptions;
using RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Items;

[Route("api/Items")]
[ApiController]

public class AddNewItems : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewItems(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewItemsCommand : IRequest<Unit>
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int AddedBy { get; set; }
        public int UomId { get; set; }
        public int ProductCategoryId { get; set; }
        public int MeatTypeId { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewItemsCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewItemsCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);
            var validateProductCategory =
                await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId,
                    cancellationToken);
            var validateUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);
            var validateMeatType =
                await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

            if (existingItem is not null)
            {
                throw new ItemAlreadyExistException();
            }

            if (validateUom is null)
            {
                throw new UomNotFoundException();
            }

            if (validateProductCategory is null)
            {
                throw new NoProductCategoryFoundException();
            }

            if (validateMeatType is null)
            {
                throw new MeatTypeNotFoundException();
            }

            var items = new Domain.Items
            {
                ItemCode = request.ItemCode,
                ItemDescription = request.ItemDescription,
                UomId = request.UomId,
                ProductSubCategoryId = request.ProductCategoryId,
                AddedBy = request.AddedBy,
                MeatTypeId = request.MeatTypeId,
                IsActive = true
            };

            await _context.Items.AddAsync(items, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;

        }
    }
    
    [HttpPost("AddNewItem")]
    public async Task<IActionResult> AddNewItem(AddNewItemsCommand command)
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
            response.Messages.Add($"Item {command.ItemCode} successfully added");
            response.Status = StatusCodes.Status200OK;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}