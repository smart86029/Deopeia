using Confluent.Kafka;
using Deopeia.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Infrastructure.Events;

public static class HostApplicationBuilderExtensions
{
    private const string EventSuffix = "Event";

    internal static IHostApplicationBuilder AddEventProducer<TContext>(
        this IHostApplicationBuilder builder
    )
        where TContext : DbContext
    {
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
        var name = typeof(TEvent).Name;
        if (name.EndsWith(EventSuffix))
        {
            name = name[..^EventSuffix.Length];
        }

        builder.AddKeyedKafkaConsumer<string, byte[]>(
            name,
            settings =>
            {
                settings.ConnectionString = builder.Configuration.GetConnectionString("kafka")!;
                settings.Config.GroupId = AssemblyUtility.ServiceName;
                settings.Config.EnableAutoCommit = false;
                settings.Config.AutoOffsetReset = AutoOffsetReset.Earliest;
                settings.Config.AllowAutoCreateTopics = true;
            }
        );

        var services = builder.Services;
        services.AddHostedService<EventSubscribeWorker>();
        services.AddKeyedTransient<IEventHandler, TEventHandler>(typeof(TEvent));
        services.Configure<EventSubscriptions>(x =>
        {
            x[name] = typeof(TEvent);
        });

        return builder;
    }
}
