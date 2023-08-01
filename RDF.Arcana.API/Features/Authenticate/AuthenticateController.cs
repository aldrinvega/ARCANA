using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Features.Authenticate;
using RDF.Arcana.API.Common;

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
    public async Task<ActionResult<AuthenticateUser.AuthenticateUserResult>> Authenticate(AuthenticateUser.AuthenticateUserQuery request)
    {

        var response = new QueryOrCommandResult<AuthenticateUser.AuthenticateUserResult>();
        try
        {
            var result = await _mediator.Send(request);
            response.Data = result;
            response.Success = true;
            response.Messages.Add("Log In successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}