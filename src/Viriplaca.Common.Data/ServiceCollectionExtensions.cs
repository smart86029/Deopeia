using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

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
}
