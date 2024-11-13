using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Application.Accounts.Deposit;

public class DepositCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IAccountRepository accountRepository
) : IRequestHandler<DepositCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountAsync(new AccountId(request.Id));
        var money = new Money(new CurrencyCode(request.CurrencyCode), request.Amount);
        account.Deposit(money);

        await _unitOfWork.CommitAsync();
    }
}
