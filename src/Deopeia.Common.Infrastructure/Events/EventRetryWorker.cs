using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Deopeia.Common.Infrastructure.Events;

internal class EventRetryWorker<TContext>(
    ILogger<EventSubscribeWorker> logger,
    IServiceProvider serviceProvider,
    IEventProducer eventProducer
) : BackgroundService
    where TContext : DbContext
{
    private readonly ILogger<EventSubscribeWorker> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IEventProducer _eventProducer = eventProducer;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await using var scope = _serviceProvider.CreateAsyncScope();

                var context = scope.ServiceProvider.GetRequiredService<TContext>();
                var eventLogs = context
                    .Set<EventLog>()
                    .Where(x => x.PublishState != PublishState.Completed)
                    .ToList();

                var tasks = eventLogs.Select(async eventLog =>
                {
                    await _eventProducer.ProduceAsync(eventLog);
                });
                await Task.WhenAll(tasks);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error executing retry worker");
            }
            finally
            {
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }
    }
}
