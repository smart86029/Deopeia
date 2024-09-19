namespace Deopeia.Quote.Application.FuturesContracts.UpdateFuturesContract;

public record UpdateFuturesContractCommand(string Mic, string TimeZone) : IRequest { }
