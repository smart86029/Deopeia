namespace Deopeia.Trading.Application.Accounts.UpdateAccount;

public record UpdateAccountCommand(Guid Id, bool IsEnabled) : IRequest { }
