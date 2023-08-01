using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

[Route("api/[controller]")]
[ApiController]

public class MeatTypeController : Controller
{
    private readonly IMediator _mediator;

    public MeatTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    
    
   


}