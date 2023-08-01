using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Authenticate;

public class MyAuthenticationFailureHandler : JwtBearerEvents
{
    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";
        
        var responseObj = new QueryOrCommandResult<object>
        {
            Status = StatusCodes.Status401Unauthorized,
            Success = false,
            Messages = new List<string> { "You are not authorized to access this resource." }
        };
        var responseText = JsonSerializer.Serialize(responseObj);
        return context.Response.WriteAsync(responseText);
    }
}