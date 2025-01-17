using Deopeia.Common.Extensions;
using Deopeia.Common.Utilities;

namespace Deopeia.Common.Events;

public class EventLog
{
    private Event? _event;

    private EventLog() { }

    public EventLog(Event @event)
    {
        var eventType = @event.GetType();

        EventId = @event.Id;
        EventTypeNamespace = eventType.Namespace!;
        EventTypeName = eventType.Name;
        EventContent = @event.ToJson();
        CreatedAt = @event.CreatedAt;
        Event = @event;
    }

    public Guid EventId { get; private init; }

    public string EventTypeNamespace { get; private init; } = string.Empty;

    public string EventTypeName { get; private init; } = string.Empty;

    public string EventContent { get; private init; } = string.Empty;

    public PublishState PublishState { get; private set; } = PublishState.Pending;

    public int PublishCount { get; private set; }

    public DateTimeOffset CreatedAt { get; private init; }

    public Event Event
    {
        get
        {
            if (_event is null)
            {
                var type = AssemblyUtility.GetType($"{EventTypeNamespace}.{EventTypeName}")!;
                _event = EventContent.ToObject(type) as Event;
            }

            return _event!;
        }
        set => _event = value;
    }

    public void Publish()
    {
        switch (PublishState)
        {
            case PublishState.Pending:
            case PublishState.Failed:
                PublishState = PublishState.InProgress;
                PublishCount++;
                break;

            case PublishState.InProgress:
                PublishCount++;
                break;

            default:
                return;
        }
    }

    public void Complete()
    {
        if (PublishState == PublishState.InProgress)
        {
            PublishState = PublishState.Completed;
        }
    }

    public void Fail()
    {
        if (PublishState != PublishState.Completed)
        {
            PublishState = PublishState.Failed;
        }
    }
}
