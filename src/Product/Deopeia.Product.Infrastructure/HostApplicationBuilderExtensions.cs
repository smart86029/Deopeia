using Microsoft.Extensions.Hosting;

namespace Deopeia.Product.Infrastructure;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        return builder.AddInfrastructure<ProductContext, ProductSeeder>();
    }
}
