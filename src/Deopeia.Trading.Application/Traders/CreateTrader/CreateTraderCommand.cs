namespace Deopeia.Trading.Application.Traders.CreateTrader;

public record CreateTraderCommand(string Name, bool IsEnabled) : IRequest { }
