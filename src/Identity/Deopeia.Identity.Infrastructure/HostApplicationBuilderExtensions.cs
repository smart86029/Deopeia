using Microsoft.Extensions.Hosting;

namespace Deopeia.Identity.Infrastructure;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        return builder.AddInfrastructure<IdentityContext, IdentitySeeder>();
    }
}
