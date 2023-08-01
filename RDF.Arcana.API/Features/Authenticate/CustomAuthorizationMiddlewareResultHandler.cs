using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Newtonsoft.Json;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Authenticate;

public class CustomAuthorizationMiddlewareResultHandler : AuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public new async Task HandleAsync(
        RequestDelegate requestDelegate,
        HttpContext httpContext,
        AuthorizationPolicy authorizationPolicy,
        PolicyAuthorizationResult policyAuthorizationResult)
    {
        // If authorization was successful, continue with the pipeline
        if (policyAuthorizationResult.Succeeded)
        {
            await requestDelegate(httpContext);
            return;
        }

        // here we are handling only the authorization failures
        var result = new QueryOrCommandResult<object>
        {
            Status = StatusCodes.Status401Unauthorized,
            Success = false,
            Messages = new List<string> { "You are not authorized to access this resource." }
        };
    
        var json = System.Text.Json.JsonSerializer.Serialize(result);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

        await httpContext.Response.WriteAsync(json);
    }
}