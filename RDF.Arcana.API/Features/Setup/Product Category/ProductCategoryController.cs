using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Product_Category;



public class ProductCategoryController : ControllerBase
{

   private readonly IMediator _mediator;

   public ProductCategoryController(IMediator mediator)
   {
      _mediator = mediator;
   }

   

  
   

   
   
}