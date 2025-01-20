using Deopeia.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Infrastructure.Events;

public static class HostApplicationBuilderExtensions
{
    private const string EventSuffix = "Event";

    private static bool _isSubscribeWorkerAdded = false;

    internal static IHostApplicationBuilder AddEventProducer<TContext>(
        this IHostApplicationBuilder builder
    )
        where TContext : DbContext
    {
        builder.ConfigureKafka();
        builder.AddKafkaProducer<string, byte[]>("kafka");

        var services = builder.Services;
        services.AddSingleton<IEventProducer, EventProducer<TContext>>();
        services.AddHostedService<EventRetryWorker<TContext>>();

        return builder;
    }

    public static IHostApplicationBuilder AddEventConsumer<TEvent, TEventHandler>(
        this IHostApplicationBuilder builder
    )
        where TEvent : Event
        where TEventHandler : class, IEventHandler<TEvent>
    {
        if (!_isSubscribeWorkerAdded)
        {
            builder.ConfigureKafka();
            builder.Services.AddHostedService<EventSubscribeWorker>();
            _isSubscribeWorkerAdded = true;
        }

        var services = builder.Services;
        services.AddKeyedTransient<IEventHandler, TEventHandler>(typeof(TEvent));
        services.Configure<EventBusSubscription>(x =>
        {
            var name = typeof(TEvent).Name;
            if (name.EndsWith(EventSuffix))
            {
                name = name[..^EventSuffix.Length];
            }
            x.EventTypes[name] = typeof(TEvent);
        });

        return builder;
    }

    private static IHostApplicationBuilder ConfigureKafka(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<EventBusSubscription>(x =>
        {
            x.ConnectionString = builder.Configuration.GetConnectionString("kafka")!;
        });

        return builder;
    }
}
