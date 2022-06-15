using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Security;
using Shared.Security.Model;

namespace Shared;

public static class ServiceExtensions
{
    public static IServiceCollection AddNotificationHandler(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();

        return services;
    }
    public static IServiceCollection AddDatabaseContext<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure();
            });
        });

        services.AddScoped<DbContext, TContext>();

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration serviceConfiguration)
    {
        services.AddHttpClient("cart", client =>
        {
            client.BaseAddress = new Uri(serviceConfiguration["Cart"]);
        });

        services.AddHttpClient("shipping", client =>
        {
            client.BaseAddress = new Uri(serviceConfiguration["Shipping"]);
        });

        services.AddHttpClient("checkout", client =>
        {
            client.BaseAddress = new Uri(serviceConfiguration["Checkout"]);
        });

        services.AddHttpClient("catalog", client =>
        {
            client.BaseAddress = new Uri(serviceConfiguration["Catalog"]);
        });

        return services;
    }
    
    public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var securityInfo = new SecurityInfoModel();
        configuration.Bind("Security", securityInfo);
        services.AddSingleton(securityInfo);

        services.AddSingleton<IIdentityService, IdentiyService>();
        services.AddScoped<IdentityModel>();
        services.AddScoped<ISecuredMethodService, SecuredMethodService>();

        return services;
    }
}
