namespace Deopeia.Common.Events;

public class EventBusSubscription
{
    public string ConnectionString { get; set; } = string.Empty;

    public Dictionary<string, Type> EventTypes { get; private init; } = [];
}
