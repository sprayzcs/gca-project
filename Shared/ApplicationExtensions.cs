using Microsoft.AspNetCore.Builder;
using Shared.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public static class ApplicationExtensions
{
    public static void UseCustomLag(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomLagMiddleware>();
    }
}
