using Deopeia.Common.Domain.Files;
using Deopeia.Common.Infrastructure.Events;
using Deopeia.Common.Infrastructure.Files;
using Deopeia.Common.Infrastructure.Localization;
using Deopeia.Common.Infrastructure.TypeHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Minio;
using Npgsql;

namespace Deopeia.Common.Infrastructure;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddInfrastructure<TContext, TSeeder>(
        this IHostApplicationBuilder builder
    )
        where TContext : DbContext
        where TSeeder : DbSeeder, new()
    {
        var database = AssemblyUtility.ServiceName;
        builder.AddNpgsqlDbContext<TContext>(
            database,
            configureDbContextOptions: options =>
            {
                options
                    .UseSnakeCaseNamingConvention()
                    .UseSeeding(
                        (context, _) =>
                        {
                            var seeder = new TSeeder();
                            seeder.Seed(context);
                        }
                    );
            }
        );
        builder.AddEventProducer<TContext>();

        var services = builder.Services;
        var configuration = builder.Configuration;
        var connectionString = configuration.GetConnectionString(database);
        services.AddScoped(serviceProvider => new NpgsqlConnection(connectionString));

        services.AddMinIO(configuration);
        services.AddRepositories<TContext>();
        services.AddLocalization<TContext>(options =>
            options.FallbackCulture = CultureInfo.GetCultureInfo("en")
        );

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        SqlMapper.AddTypeHandler(new CollectionTypeHandler<decimal>());
        SqlMapper.AddTypeHandler(new CultureInfoTypeHandler());
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());

        return builder;
    }

    private static IServiceCollection AddMinIO(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMinio(client =>
            client
                .WithEndpoint(configuration.GetConnectionString("MinIO"))
                .WithCredentials(configuration["MinIO:AccessKey"], configuration["MinIO:SecretKey"])
                .WithSSL(false)
        );

        return services;
    }

    private static IServiceCollection AddRepositories<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        var types = AssemblyUtility
            .GetTypes(".Infrastructure")
            .Where(x =>
                x.IsAssignableToGenericType(typeof(IRepository<,>))
                || x.IsAssignableTo(typeof(IUnitOfWork))
                || x.Name.EndsWith("Service")
            );
        foreach (var type in types)
        {
            var interfaceTypes = type.GetInterfaces()
                .Where(x => x != typeof(IRepository<,>) && x != typeof(IUnitOfWork));
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

        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en");

        return services;
    }
}
