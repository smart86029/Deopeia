using System.Net.Sockets;
using System.Text;
using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Deopeia.Common.Infrastructure.Events;

internal class EventBus(
    ILogger<EventBus> logger,
    IServiceProvider serviceProvider,
    IOptions<EventBusOptions> options,
    IOptions<EventBusSubscription> subscriptionOptions
) : IEventBus, IHostedService, IDisposable
{
    private const string ExchangeName = "deopeia";

    private readonly ILogger<EventBus> _logger = logger;
    private readonly ResiliencePipeline _pipeline = CreateResiliencePipeline(
        options.Value.RetryCount
    );
    private readonly string _queueName = options.Value.SubscriptionClientName;
    private readonly EventBusSubscription _subscription = subscriptionOptions.Value;

    private IConnection? _connection;
    private IModel? _consumerChannel;

    public Task PublishAsync(Event @event)
    {
        using var channel =
            _connection?.CreateModel()
            ?? throw new InvalidOperationException("RabbitMQ connection is not open");
        channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

        var routingKey = @event.GetType().Name;
        var body = @event.ToUtf8Bytes();

        return _pipeline.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish(
                exchange: ExchangeName,
                routingKey: routingKey,
                mandatory: true,
                basicProperties: properties,
                body: body
            );

            return Task.CompletedTask;
        });
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = Task.Factory.StartNew(
            () =>
            {
                try
                {
                    _logger.LogInformation("Starting RabbitMQ connection on a background thread");

                    _connection = serviceProvider.GetRequiredService<IConnection>();
                    if (!_connection.IsOpen)
                    {
                        return;
                    }

                    _consumerChannel = _connection.CreateModel();
                    _consumerChannel.CallbackException += (sender, e) =>
                    {
                        _logger.LogWarning(e.Exception, "Error with RabbitMQ consumer channel");
                    };

                    _consumerChannel.ExchangeDeclare(exchange: ExchangeName, type: "direct");
                    _consumerChannel.QueueDeclare(
                        queue: _queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
                    consumer.Received += OnMessageReceived;

                    _consumerChannel.BasicConsume(
                        queue: _queueName,
                        autoAck: false,
                        consumer: consumer
                    );

                    foreach (var (eventName, _) in _subscription.EventTypes)
                    {
                        _consumerChannel.QueueBind(
                            queue: _queueName,
                            exchange: ExchangeName,
                            routingKey: eventName
                        );
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error starting RabbitMQ connection");
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
        _consumerChannel?.Dispose();
        _connection?.Dispose();
    }

    private static ResiliencePipeline CreateResiliencePipeline(int retryCount)
    {
        var retryOptions = new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder()
                .Handle<BrokerUnreachableException>()
                .Handle<SocketException>(),
            MaxRetryAttempts = retryCount,
            DelayGenerator = (context) =>
            {
                var seconds = Math.Pow(2, context.AttemptNumber);
                var delay = TimeSpan.FromSeconds(seconds) as TimeSpan?;

                return ValueTask.FromResult(delay);
            },
        };

        return new ResiliencePipelineBuilder().AddRetry(retryOptions).Build();
    }

    private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            if (
                message.Contains(
                    "throw-fake-exception",
                    StringComparison.InvariantCultureIgnoreCase
                )
            )
            {
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
            }

            await using var scope = serviceProvider.CreateAsyncScope();

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

        _consumerChannel?.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }
}
