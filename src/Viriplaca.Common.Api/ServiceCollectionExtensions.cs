using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Viriplaca.Common.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddOptions<JwtOptions>().BindConfiguration("Jwt");
        services.AddOptions<ConnectionStringOptions>().BindConfiguration("ConnectionStrings");
        services.AddProblemDetails();
        services.AddExceptionHandler<ExceptionHandler>();

        var assemblies = Assembly.GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Viriplaca."))
            .Select(Assembly.Load)
            .ToArray();
        services.AddMediatR(options => options.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}
