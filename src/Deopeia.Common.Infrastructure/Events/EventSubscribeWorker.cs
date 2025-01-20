using System.Text;
using Confluent.Kafka;
using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;

namespace Deopeia.Common.Infrastructure.Events;

internal class EventSubscribeWorker(
    ILogger<EventSubscribeWorker> logger,
    IServiceProvider serviceProvider,
    IOptions<EventBusSubscription> subscriptionOptions
) : BackgroundService
{
    private readonly ILogger<EventSubscribeWorker> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly EventBusSubscription _subscription = subscriptionOptions.Value;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var tasks = _subscription.EventTypes.Select(async x =>
        {
            using var consumer = new ConsumerBuilder<string, byte[]>(
                new ConsumerConfig
                {
                    BootstrapServers = _subscription.ConnectionString,
                    GroupId = AssemblyUtility.ServiceName,
                    EnableAutoCommit = false,
                }
            ).Build();
            await SubscribeAsync(consumer, x.Key);

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
        });

        await Task.WhenAll(tasks);
    }

    private async Task SubscribeAsync(IConsumer<string, byte[]> consumer, string topic)
    {
        var retryPolicy = Policy
            .Handle<KafkaException>(x => x.Error.Code == ErrorCode.UnknownTopicOrPart)
            .WaitAndRetryForeverAsync(
                retryNumber => TimeSpan.FromSeconds(5),
                (exception, timeSpan) =>
                {
                    _logger.LogWarning(
                        "Topic {topic} has not been created yet. Will retry in {TotalSeconds} seconds.",
                        topic,
                        timeSpan.TotalSeconds
                    );
                }
            );
        await retryPolicy.ExecuteAsync(async () =>
        {
            await Task.CompletedTask;
            consumer.Subscribe(topic);
            _logger.LogInformation("Topic {topic} successfully subscribed.", topic);
        });
    }

    private async Task HandleAsync(string eventName, string message)
    {
        try
        {
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

            await using var scope = _serviceProvider.CreateAsyncScope();
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
