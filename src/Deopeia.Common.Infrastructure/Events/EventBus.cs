using System.Text;
using Confluent.Kafka;
using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Deopeia.Common.Infrastructure.Events;

internal class EventBus(
    ILogger<EventBus> logger,
    IServiceProvider serviceProvider,
    IProducer<string, byte[]> producer,
    IConsumer<string, byte[]> consumer,
    IOptions<EventBusSubscription> subscriptionOptions
) : IEventBus, IHostedService, IDisposable
{
    private const string ExchangeName = "deopeia";

    private readonly ILogger<EventBus> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IProducer<string, byte[]> _producer = producer;
    private readonly IConsumer<string, byte[]> _consumer = consumer;

    private readonly EventBusSubscription _subscription = subscriptionOptions.Value;

    public async Task PublishAsync(Event @event)
    {
        var key = @event.GetType().Name;
        var value = @event.ToUtf8Bytes();

        await _producer.ProduceAsync(
            ExchangeName,
            new Message<string, byte[]> { Key = key, Value = value }
        );
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = Task.Factory.StartNew(
            async () =>
            {
                _consumer.Subscribe(ExchangeName);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);
                        var key = consumeResult.Message.Key;
                        var value = Encoding.UTF8.GetString(consumeResult.Message.Value);
                        await Handle(key, value);
                        _consumer.Commit();
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, "Error starting Kafka connection");
                    }
                }
            },
            TaskCreationOptions.LongRunning
        );

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _consumer?.Dispose();
    }

    private async Task Handle(string eventName, string message)
    {
        try
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            if (!_subscription.EventTypes.TryGetValue(eventName, out var eventType))
            {
                _logger.LogWarning(
                    "Unable to resolve event type for event name {EventName}",
                    eventName
                );
                return;
            }

            if (message.ToObject(eventType) is not Event @event)
            {
                return;
            }

            var handlers = scope.ServiceProvider.GetKeyedServices<IEventHandler>(eventType);
            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Error Processing message \"{Message}\"", message);
        }
    }
}
