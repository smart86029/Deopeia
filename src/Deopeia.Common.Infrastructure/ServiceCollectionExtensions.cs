using System.Reflection;
using Deopeia.Common.Domain.Files;
using Deopeia.Common.Infrastructure.Files;
using Deopeia.Common.Infrastructure.Localization;
using Deopeia.Common.Infrastructure.TypeHandlers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Minio;

namespace Deopeia.Common.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IHostApplicationBuilder AddInfrastructure<TContext, TSeeder>(
        this IHostApplicationBuilder builder
    )
        where TContext : DbContext
        where TSeeder : class, IDbSeeder<TContext>
    {
        var database = Assembly.GetEntryAssembly()!.FullName!.Split('.')[1];
        builder.AddSqlServerDbContext<TContext>(database);

        var services = builder.Services;
        var connectionString = builder.Configuration.GetConnectionString(database);
        services.AddScoped(serviceProvider => new SqlConnection(connectionString));

        services.AddScoped<IDbSeeder<TContext>, TSeeder>();
        services.AddHostedService<MigrationWorker<TContext>>();

        var minIOOptions = builder.Configuration.GetSection("MinIO").Get<MinIOOptions>()!;
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

        return builder;
    }

    private static IServiceCollection AddRepositories<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        var types = Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Deopeia.") && x.Name.EndsWith(".Infrastructure"))
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
