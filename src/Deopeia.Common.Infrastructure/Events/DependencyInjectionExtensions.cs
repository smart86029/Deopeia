using Deopeia.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Infrastructure.Events;

public static class DependencyInjectionExtensions
{
    public static IEventBusBuilder AddEventBus(this IHostApplicationBuilder builder)
    {
        builder.AddKafkaProducer<string, byte[]>("kafka");

        var services = builder.Services;
        services.AddSingleton<IEventBus, EventBus>();
        services.AddHostedService(sp => (EventBus)sp.GetRequiredService<IEventBus>());

        services.Configure<EventBusSubscription>(x =>
        {
            x.ConnectionString = builder.Configuration.GetConnectionString("kafka")!;
        });

        return new EventBusBuilder(builder.Services);
    }
}
