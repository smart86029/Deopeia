namespace Deopeia.Finance.Bff.Events;

public record CandleChangedEvent(
    string Symbol,
    int TimeFrame,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal Volume
) : Event { }
