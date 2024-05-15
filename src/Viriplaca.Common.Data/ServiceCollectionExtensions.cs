using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Minio;
using Viriplaca.Common.Data.Files;
using Viriplaca.Common.Data.Localization;
using Viriplaca.Common.Data.TypeHandlers;
using Viriplaca.Common.Files;

namespace Viriplaca.Common.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData<TContext, TSeeder>(
        this IServiceCollection services,
        MinIOOptions minIOOptions
    )
        where TContext : DbContext
        where TSeeder : class, IDbSeeder<TContext>
    {
        services.AddDbContext<TContext>(
            (serviceProvider, optionsBuilder) =>
            {
                var connectionStringOptions = serviceProvider
                    .GetRequiredService<IOptions<ConnectionStringOptions>>()
                    .Value;
                optionsBuilder.UseSqlServer(connectionStringOptions.Database);
            }
        );
        services.AddScoped(serviceProvider =>
        {
            var connectionStringOptions = serviceProvider
                .GetRequiredService<IOptions<ConnectionStringOptions>>()
                .Value;

            return new SqlConnection(connectionStringOptions.Database);
        });

        services.AddScoped<IDbSeeder<TContext>, TSeeder>();
        services.AddHostedService<MigrationWorker<TContext>>();
        services.AddMinio(client =>
            client
                .WithEndpoint(minIOOptions.Endpoint)
                .WithCredentials(minIOOptions.AccessKey, minIOOptions.SecretKey)
                .WithSSL(false)
        );

        services.AddRepositories<TContext>();
        services.AddLocalization<TContext>(options =>
            options.FallbackCulture = CultureInfo.GetCultureInfo("en-US")
        );

        SqlMapper.AddTypeHandler(new CultureInfoTypeHandler());
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());

        return services;
    }

    private static IServiceCollection AddRepositories<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        var types = Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Viriplaca.") && x.Name.EndsWith(".Data"))
            .Select(Assembly.Load)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType)
            .Where(x =>
                x.IsAssignableToGenericType(typeof(IRepository<>))
                || x.IsAssignableTo(typeof(IUnitOfWork))
            );
        foreach (var type in types)
        {
            var interfaceTypes = type.GetInterfaces()
                .Where(x => x != typeof(IRepository<>) && x != typeof(IUnitOfWork));
            foreach (var interfaceType in interfaceTypes)
            {
                services.AddScoped(interfaceType, type);
            }
        }

        services.TryAddScoped<IImageRepository, ImageRepository<TContext>>();

        return services;
    }

    private static IServiceCollection AddLocalization<TContext>(
        this IServiceCollection services,
        Action<LocalizationOptions> configureOptions
    )
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
