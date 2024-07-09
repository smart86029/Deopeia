using System.Reflection;
using Coravel;
using Microsoft.Extensions.DependencyInjection;

namespace Deopeia.Common.Worker;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorker(this IServiceCollection services)
    {
        services.AddScheduler();

        var assemblies = Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Deopeia."))
            .Select(Assembly.Load)
            .ToArray();

        return services;
    }
}
