using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shared;

public static class HostExtensions
{
    public static async Task MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
    {
        var context = host.Services.CreateScope().ServiceProvider.GetRequiredService<TContext>();
        await context.Database.MigrateAsync();
    }
}
