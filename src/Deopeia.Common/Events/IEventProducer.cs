namespace Deopeia.Common.Events;

public interface IEventProducer
{
    Task ProduceAsync(Event @event);

    Task ProduceAsync(EventLog eventLog);
}
