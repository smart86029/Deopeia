using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Application.Accounts.Withdraw;

public class WithdrawCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IAccountRepository accountRepository
) : IRequestHandler<WithdrawCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountAsync(new AccountId(request.Id));
        var money = new Money(new CurrencyCode(request.CurrencyCode), request.Amount);
        account.Withdraw(money);

        await _unitOfWork.CommitAsync();
    }
}
