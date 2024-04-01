using Microsoft.AspNetCore.Mvc;

namespace RDF.Arcana.API.Common;

public class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>();
}
