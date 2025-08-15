using Deopeia.Common.Application;
using Deopeia.Common.Domain.Files;
using Deopeia.Common.Infrastructure.Files;
using Deopeia.Common.Infrastructure.TypeHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
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
        builder.AddNpgsqlDbContext<TContext, TSeeder>(database);

        var services = builder.Services;
        var configuration = builder.Configuration;
        var connectionString = configuration.GetConnectionString(database);
        services.AddScoped(serviceProvider => new NpgsqlConnection(connectionString));

        services.AddMinIO(configuration);
        services.AddRepositories<TContext>();

        ConfigureDapper();

        return builder;
    }

    private static void AddNpgsqlDbContext<TContext, TSeeder>(
        this IHostApplicationBuilder builder,
        string database
    )
        where TContext : DbContext
        where TSeeder : DbSeeder, new()
    {
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
    }

    private static void AddMinIO(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMinio(client =>
            client
                .WithEndpoint(configuration.GetConnectionString("MinIO"))
                .WithCredentials(configuration["MinIO:AccessKey"], configuration["MinIO:SecretKey"])
                .WithSSL(false)
        );
    }

    private static void AddRepositories<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        var types = AssemblyUtility
            .GetTypes()
            .Where(x =>
                x.IsAssignableToGenericType(typeof(IRepository<,>))
                || x.IsAssignableTo(typeof(IUnitOfWork))
                || x.Name.EndsWith("Service")
            );
        foreach (var type in types)
        {
            var interfaceTypes = type.GetInterfaces().Where(x => x != typeof(IRepository<,>));
            foreach (var interfaceType in interfaceTypes)
            {
                services.AddScoped(interfaceType, type);
            }
        }

        services.TryAddScoped<IImageRepository, ImageRepository<TContext>>();
    }

    private static void ConfigureDapper()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        SqlMapper.AddTypeHandler(new CollectionTypeHandler<decimal>());
        SqlMapper.AddTypeHandler(new CultureInfoTypeHandler());
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
    }
}
