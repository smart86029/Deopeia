using System.Reflection;
using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Deopeia.Common.Infrastructure.Events;

public static class DependencyInjectionExtensions
{
    public static IEventBusBuilder AddEventBus(this IHostApplicationBuilder builder)
    {
        builder.AddKafkaProducer<string, byte[]>("kafka");
        builder.AddKafkaConsumer<string, byte[]>(
            "kafka",
            settings =>
            {
                settings.Config.GroupId = AssemblyUtility.ServiceName;
                settings.Config.EnableAutoCommit = false;
            }
        );

        var services = builder.Services;
        services.AddSingleton<IEventBus, EventBus>();
        services.AddSingleton<IHostedService>(sp => (EventBus)sp.GetRequiredService<IEventBus>());

        return new EventBusBuilder(builder.Services);
    }
}
