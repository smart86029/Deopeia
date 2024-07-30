namespace Deopeia.Common.Events;

public interface IEventHandler
{
    Task Handle(Event @event);
}

public interface IEventHandler<in TEvent> : IEventHandler
    where TEvent : Event
{
    Task Handle(TEvent @event);

    Task IEventHandler.Handle(Event @event) => Handle((TEvent)@event);
}
