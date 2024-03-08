using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Reflection;
using Viriplaca.Common.Data.Localization;

namespace Viriplaca.Common.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData<TContext, TSeeder>(this IServiceCollection services, Assembly assembly)
        where TContext : DbContext
        where TSeeder : class, IDbSeeder<TContext>
    {
        services.AddDbContext<TContext>((serviceProvider, optionsBuilder) =>
        {
            var connectionStringOptions = serviceProvider.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
            optionsBuilder.UseSqlServer(connectionStringOptions.Database);
        });
        services.AddScoped((serviceProvider) =>
        {
            var connectionStringOptions = serviceProvider.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;

            return new SqlConnection(connectionStringOptions.Database);
        });

        services.AddScoped<IDbSeeder<TContext>, TSeeder>();
        services.AddHostedService<MigrationWorker<TContext>>();

        services.AddRepositories(assembly);
        services.AddLocalization<TContext>(options => options.FallbackCulture = CultureInfo.GetCultureInfo("en-US"));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        var repositoryTypes = assembly
            .GetTypes()
            .Where(x => x.Name.EndsWith("Repository"));
        foreach (var repositoryType in repositoryTypes)
        {
            foreach (var interfaceType in repositoryType.GetInterfaces())
            {
                services.AddScoped(interfaceType, repositoryType);
            }
        }

        return services;
    }

    private static IServiceCollection AddLocalization<TContext>(this IServiceCollection services, Action<LocalizationOptions> configureOptions)
        where TContext : DbContext
    {
        services.Configure(configureOptions);
        services.AddOptions();
        services.TryAddSingleton<IStringLocalizerFactory, StringLocalizerFactory<TContext>>();
        services.TryAddTransient(services =>
        {
            var factory = services.GetRequiredService<IStringLocalizerFactory>();
            return factory.Create(typeof(LocaleResource));
        });

        return services;
    }
}
