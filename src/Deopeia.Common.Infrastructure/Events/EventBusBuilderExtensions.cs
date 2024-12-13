using System.Diagnostics.CodeAnalysis;
using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Deopeia.Common.Infrastructure.Events;

public static class EventBusBuilderExtensions
{
    public static IEventBusBuilder AddSubscription<
        TEvent,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
            TEventHandler
    >(this IEventBusBuilder eventBusBuilder)
        where TEvent : Event
        where TEventHandler : class, IEventHandler<TEvent>
    {
        return eventBusBuilder.AddSubscription<TEvent, TEventHandler>(1);
    }

    public static IEventBusBuilder AddSubscription<
        TEvent,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
            TEventHandler
    >(this IEventBusBuilder eventBusBuilder, int batchSize)
        where TEvent : Event
        where TEventHandler : class, IEventHandler<TEvent>
    {
        eventBusBuilder.Services.AddKeyedTransient<IEventHandler, TEventHandler>(typeof(TEvent));

        eventBusBuilder.Services.Configure<EventBusSubscription>(x =>
        {
            x.EventTypes[typeof(TEvent).Name] = typeof(TEvent);
        });

        return eventBusBuilder;
    }
}
