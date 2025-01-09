namespace Deopeia.Trading.Application.Traders.UpdateTrader;

public record UpdateTraderCommand(Guid Id, string Name, bool IsEnabled) : IRequest { }
