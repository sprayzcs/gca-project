using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Middleware
{
    internal class CustomLagMiddleware
    {
        private static readonly TimeSpan MIN_LAG = TimeSpan.FromMilliseconds(100);
        private static readonly TimeSpan MAX_LAG = TimeSpan.FromSeconds(10);

        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLagMiddleware> _logger;

        public CustomLagMiddleware(RequestDelegate next, ILogger<CustomLagMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            TimeSpan delay = GetCustomLagTime();
            _logger.LogDebug("Delaying request '{id}' to '{path}' by '{delay}' milliseconds", httpContext.TraceIdentifier, httpContext.Request.Path, delay.TotalMilliseconds);

            await Task.Delay(delay);
            await _next(httpContext);
        }

        private static TimeSpan GetCustomLagTime()
        {
            return TimeSpan.FromMilliseconds(RandomNumberGenerator.GetInt32((int)MIN_LAG.TotalMilliseconds, (int)MAX_LAG.TotalMilliseconds));
        }
    }
}
