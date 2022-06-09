using Microsoft.AspNetCore.Builder;
using Shared.Middleware;

namespace Shared
{
    public static class ApplicationExtensions
    {
        public static void UseCustomLag(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomLagMiddleware>();
        }
    }
}
