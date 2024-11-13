namespace Deopeia.Trading.Application.Accounts.Deposit;

public record DepositCommand(Guid Id, string CurrencyCode, decimal Amount) : IRequest { }
