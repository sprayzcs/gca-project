using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure;
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
    public static IServiceCollection AddDatabaseContext<TContext>(this IServiceCollection services, string connectionString, string serviceName) where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure();
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", serviceName);
            });
        });

        services.AddScoped<DbContext, TContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    /// <summary>
    /// Adds the http clients with important headers, base addresses etc.
    /// Important: Add AFTER the security services has been added
    /// </summary>
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration serviceConfiguration)
    {
        var provider = services.BuildServiceProvider();
        var identityService = provider.GetRequiredService<IIdentityService>();
        var idToken = AuthenticationHeaderValue.Parse($"Bearer {identityService.CreateIdentityToken()}");

        services.AddHttpClient("cart", client =>
        {
            client.BaseAddress = new Uri(serviceConfiguration["Cart"]);
            client.DefaultRequestHeaders.Authorization = idToken;
        });

        services.AddHttpClient("shipping", client =>
        {
            client.BaseAddress = new Uri(serviceConfiguration["Shipping"]);
            client.DefaultRequestHeaders.Authorization = idToken;
        });

        services.AddHttpClient("checkout", client =>
        {
            client.BaseAddress = new Uri(serviceConfiguration["Checkout"]);
            client.DefaultRequestHeaders.Authorization = idToken;
        });

        services.AddHttpClient("catalog", client =>
        {
            client.BaseAddress = new Uri(serviceConfiguration["Catalog"]);
            client.DefaultRequestHeaders.Authorization = idToken;
        });

        return services;
    }

    public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var securityInfo = new SecurityInfoModel();
        configuration.Bind("Security", securityInfo);
        services.AddSingleton(securityInfo);

        services.AddSingleton<IIdentityService, IdentityService>();
        services.AddScoped<IdentityModel>();
        services.AddScoped<ISecuredMethodService, SecuredMethodService>();

        return services;
    }
}
