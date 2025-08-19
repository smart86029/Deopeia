using Deopeia.Common.Application;
using Deopeia.Common.Domain.Files;
using Deopeia.Common.Infrastructure.Dapper;
using Deopeia.Common.Infrastructure.Files;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
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

        services.AddObjectStorage(configuration);
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

    private static void AddObjectStorage(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<S3Options>(options => configuration.GetSection("S3").Bind(options));
        services.AddSingleton<IObjectStorage, S3ObjectStorage>();
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
