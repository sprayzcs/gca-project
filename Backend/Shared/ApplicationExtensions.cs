using Microsoft.AspNetCore.Builder;
using Shared.Middleware;
using Shared.Security;

namespace Shared;

public static class ApplicationExtensions
{
    public static void UseCustomLag(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomLagMiddleware>();
    }
    
    public static IApplicationBuilder AddIdentityMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<IdentityMiddleware>();
        return app;
    }
}
