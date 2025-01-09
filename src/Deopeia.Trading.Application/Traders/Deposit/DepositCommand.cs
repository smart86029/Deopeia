namespace Deopeia.Trading.Application.Traders.Deposit;

public record DepositCommand(Guid Id, string CurrencyCode, decimal Amount) : IRequest { }
