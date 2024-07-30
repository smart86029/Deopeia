namespace Deopeia.Common.Events;

public class EventBusSubscription
{
    public Dictionary<string, Type> EventTypes { get; private init; } = [];
}
