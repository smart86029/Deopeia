using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Deopeia.Common.Infrastructure;

public class MigrationWorker<TContext>(IServiceProvider serviceProvider) : BackgroundService
    where TContext : DbContext
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var seeder = serviceProvider.GetService<IDbSeeder<TContext>>();
        if (seeder is null)
        {
            return;
        }

        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var context = serviceProvider.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation(
                "Migrating database associated with context {DbContextName}",
                typeof(TContext).Name
            );

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await context.Database.MigrateAsync();
                await seeder.SeedAsync(context);
            });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "An error occurred while migrating the database used on context {DbContextName}",
                typeof(TContext).Name
            );
            throw;
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
