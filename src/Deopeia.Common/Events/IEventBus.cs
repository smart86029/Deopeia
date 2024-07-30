namespace Deopeia.Common.Events;

public interface IEventBus
{
    Task PublishAsync(Event @event);
}
