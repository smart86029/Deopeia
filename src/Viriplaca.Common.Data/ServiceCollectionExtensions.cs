using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Viriplaca.Common.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData<TDbContext>(this IServiceCollection services, Assembly assembly)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>((serviceProvider, optionsBuilder) =>
        {
            var connectionStringOptions = serviceProvider.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
            optionsBuilder.UseSqlServer(connectionStringOptions.Database);
        });
        //services.AddScoped<IHRUnitOfWork, HRUnitOfWork>();
        //services.AddScoped(x =>
        //{
        //    var connectionStringOptions = x.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;

        //    return new MySqlConnection(connectionStringOptions.Gaming);
        //});

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(assembly);
        });

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
