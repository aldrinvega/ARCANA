using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RDF.Arcana.API.Features.Authenticate;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<ActionResult<AuthenticateUser.AuthenticateUserResult>> Authenticate(
        AuthenticateUser.AuthenticateUserQuery request)
    {
        try
        {
            var result = await _mediator.Send(request);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}