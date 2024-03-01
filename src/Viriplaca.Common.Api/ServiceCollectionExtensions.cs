using Microsoft.Extensions.DependencyInjection;

namespace Viriplaca.Common.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddOptions<ConnectionStringOptions>().BindConfiguration("ConnectionStrings");

        return services;
    }
}
