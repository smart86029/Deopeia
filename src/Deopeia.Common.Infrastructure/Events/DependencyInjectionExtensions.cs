using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Infrastructure.Events;

public static class DependencyInjectionExtensions
{
    private const string SectionName = "EventBus";

    public static IEventBusBuilder AddEventBus(
        this IHostApplicationBuilder builder,
        string connectionName
    )
    {
        builder.AddRabbitMQClient(
            connectionName,
            configureConnectionFactory: factory => factory.DispatchConsumersAsync = true
        );

        var services = builder.Services;
        services.AddOptions<EventBusOptions>();
        //services.Configure<EventBusOptions>(builder.Configuration.GetSection(SectionName));
        services.AddSingleton<IEventBus, EventBus>();
        services.AddSingleton<IHostedService>(sp => (EventBus)sp.GetRequiredService<IEventBus>());

        return new EventBusBuilder(builder.Services);
    }
}
