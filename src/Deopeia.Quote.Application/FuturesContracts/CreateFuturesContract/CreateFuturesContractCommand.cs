namespace Deopeia.Quote.Application.FuturesContracts.CreateFuturesContract;

public record CreateFuturesContractCommand(string Mic, string TimeZone) : IRequest { }
