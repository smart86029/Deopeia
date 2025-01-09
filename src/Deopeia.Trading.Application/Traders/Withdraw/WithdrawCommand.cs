namespace Deopeia.Trading.Application.Traders.Withdraw;

public record WithdrawCommand(Guid Id, string CurrencyCode, decimal Amount) : IRequest { }
