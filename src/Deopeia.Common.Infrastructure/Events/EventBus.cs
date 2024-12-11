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
    IOptions<EventBusSubscription> subscriptionOptions
) : BackgroundService, IEventBus, IDisposable
{
    private const string ExchangeName = "deopeia";

    private readonly ILogger<EventBus> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IProducer<string, byte[]> _producer = producer;

    private readonly EventBusSubscription _subscription = subscriptionOptions.Value;

    public async Task PublishAsync(Event @event)
    {
        var key = @event.GetType().Name;
        var value = @event.ToUtf8Bytes();

        await _producer.ProduceAsync(key, new Message<string, byte[]> { Key = key, Value = value });
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.BeginScope("! {Name}", _producer.Name);
        var tasks = _subscription.EventTypes.Select(x =>
            Task.Run(async () =>
            {
                using var consumer = new ConsumerBuilder<string, byte[]>(
                    new ConsumerConfig
                    {
                        BootstrapServers = _producer.Name,
                        GroupId = AssemblyUtility.ServiceName,
                        EnableAutoCommit = false,
                    }
                ).Build();

                consumer.Subscribe(x.Key);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken);
                        var key = consumeResult.Message.Key;
                        var value = Encoding.UTF8.GetString(consumeResult.Message.Value);
                        await HandleAsync(key, value);
                        consumer.Commit();
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, "Error starting Kafka connection");
                    }
                }
            })
        );
        await Task.WhenAll(tasks);
    }

    private async Task HandleAsync(string eventName, string message)
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
