using Confluent.Kafka;
using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Deopeia.Common.Infrastructure.Events;

internal class EventProducer<TContext>(
    IServiceProvider serviceProvider,
    IProducer<string, byte[]> producer
) : IEventProducer
    where TContext : DbContext
{
    private const string EventSuffix = "Event";

    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IProducer<string, byte[]> _producer = producer;

    public async Task ProduceAsync(Event @event)
    {
        var key = @event.GetType().Name;
        if (key.EndsWith(EventSuffix))
        {
            key = key[..^EventSuffix.Length];
        }

        var value = @event.ToUtf8Bytes();
        await _producer.ProduceAsync(key, new Message<string, byte[]> { Key = key, Value = value });
    }

    public async Task ProduceAsync(EventLog eventLog)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        eventLog.Publish();
        context.Set<EventLog>().Update(eventLog);
        await context.SaveChangesAsync();

        var key = eventLog.EventTypeName;
        var value = eventLog.Event.ToUtf8Bytes();
        await _producer.ProduceAsync(key, new Message<string, byte[]> { Key = key, Value = value });

        eventLog.Complete();
        context.Set<EventLog>().Update(eventLog);
        await context.SaveChangesAsync();
    }
}
