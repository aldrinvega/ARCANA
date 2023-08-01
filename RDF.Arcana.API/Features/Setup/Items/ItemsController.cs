using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Items;

[Route("api/[controller]")]
[ApiController]

public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    
   

    
    
    
}