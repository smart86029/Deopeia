using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Infrastructure;

public static class HostExtensions
{
    public static IHost Migrate<TContext>(this IHost host)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TContext>();
        db.Database.Migrate();
        return host;
    }
}
