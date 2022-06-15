using Microsoft.AspNetCore.Http;
using Shared.Security.Model;

namespace Shared.Security;

public class IdentityMiddleware
{
    private readonly RequestDelegate _next;

    public IdentityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // If no or an invalid token was provided, the service can still be used. But some endpoints can be closed down
    public async Task InvokeAsync(HttpContext context, IIdentityService identityService, IdentityModel identity)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            await _next(context);
            return;
        }

        if (!identityService.ValidateIdentityToken(token, out var serviceName))
        {
            await _next(context);
            return;
        }

        identity.Authenticated = true;
        identity.ServiceName = serviceName;
        await _next(context);
    }
}
