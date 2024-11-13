namespace Deopeia.Trading.Application.Accounts.Withdraw;

public record WithdrawCommand(Guid Id, string CurrencyCode, decimal Amount) : IRequest { }
