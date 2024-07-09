using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Application;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        var assemblies = Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Deopeia."))
            .Select(Assembly.Load)
            .ToArray();
        builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblies(assemblies));

        return builder;
    }
}
