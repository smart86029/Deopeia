using Deopeia.Common.Application.Validation;
using Deopeia.Common.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Deopeia.Common.Application;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        var assemblies = AssemblyUtility.GetAssemblies().ToArray();
        builder.Services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(assemblies);
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        builder.Services.AddSingleton<
            IConfigureOptions<ValidationOptions>,
            ValidationConfiguration<ValidationOptions>
        >();

        foreach (var assembly in assemblies)
        {
            builder.Services.AddValidatorsFromAssembly(assembly);
        }

        return builder;
    }
}
