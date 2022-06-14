using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Security.Model;

namespace Shared.Security;

public static class SecurityServiceExtension
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var securitySection = configuration.GetSection("Security").Value;
        var infoModel = JsonSerializer.Deserialize<SecurityInfoModel>(securitySection);
        services.AddSingleton(infoModel!);

        services.AddSingleton<IIdentityService, IdentiyService>();
        services.AddScoped<IdentityModel>();

        return services;
    }
}
