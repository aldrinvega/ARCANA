using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Meat_Type;
using RDF.Arcana.API.Features.Setup.Product_Sub_Category;
using RDF.Arcana.API.Features.Setup.UOM;
using static RDF.Arcana.API.Features.Client.All.UpdateClientAttachment.UpdateAttachmentsCommand;
using B2Net;
using Microsoft.Extensions.Options;

namespace RDF.Arcana.API.Features.Setup.Items;

[Route("api/Items")]
[ApiController]

public class UpdateItem : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateItem(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateItemCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ModifiedBy { get; set; }
        public int UomId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int MeatTypeId { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectivityDate { get; set; }
        public IFormFile ItemImageLink { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateItemCommand, Result>
    {
        private readonly ArcanaDbContext _context;
        private readonly Cloudinary _cloudinary;
    
        public Handler(ArcanaDbContext context, IOptions<CloudinaryOptions> options)
        {
            _context = context;
            var account = new Account(
            options.Value.Cloudname,
            options.Value.ApiKey,
                options.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }
    
        public async Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
         {
             var existingItem = await _context.Items
                     .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
             var validateMeatType = await _context.MeatTypes
                 .FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);
             var validateUom = await _context.Uoms
                 .FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);
             var validateProductSubCategoryId = await _context.ProductSubCategories
                 .FirstOrDefaultAsync(x => x.Id == request.ProductSubCategoryId,
                     cancellationToken);

             if (validateUom is null)
             {
                 return UomErrors.NotFound();
             }

             if (validateProductSubCategoryId is null)
             {
                 return ProductSubCategoryErrors.NotFound();
             }
             if (validateMeatType is null )
             {
                 return MeatTypeErrors.NotFound();
             }
         
             if (existingItem.ItemCode != request.ItemCode)
             {
                 var validateItemCode =
                     await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);
                 if (validateItemCode is not null)
                 {
                     return ItemErrors.NotFound(request.Id);
                 }
             }

            // If the new attachment is a file, upload it and update the relevant record in the database.
            if (request.ItemImageLink is not null)
            {
                await using var stream = request.ItemImageLink.OpenReadStream();

                var attachmentsParams = new ImageUploadParams
                {
                    File = new FileDescription(request.ItemImageLink.FileName, stream),
                    PublicId =
                        $"{request.ItemImageLink.FileName}"
                };

                var attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams);

                // If an existing ClientDocument already exists, update DocumentPath
                existingItem.ItemImageLink = attachmentsUploadResult.SecureUrl.ToString();
            }

            existingItem.ItemCode = request.ItemCode;
            existingItem.ItemDescription = request.ItemDescription;
            existingItem.UomId = request.UomId;
            existingItem.ProductSubCategoryId = request.ProductSubCategoryId;
            existingItem.MeatTypeId = request.MeatTypeId;
            existingItem.UpdatedAt = DateTime.Now;
            existingItem.ModifiedBy = request.ModifiedBy ?? Roles.Admin;

            await _context.SaveChangesAsync(cancellationToken);

             return Result.Success();
         }
    }

    [HttpPut("UpdateItem/{id:int}")]
    public async Task<IActionResult> Update([FromForm]UpdateItemCommand command, [FromRoute] int id)
    {
        try
        {
            command.ModifiedBy = User.Identity?.Name;
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return Ok(e.Message);
        }
    }

}