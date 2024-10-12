using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Application.Accounts.CreateAccount;

public class CreateAccountCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IAccountRepository accountRepository
) : IRequestHandler<CreateAccountCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Account(
            request.AccountNumber,
            request.IsEnabled,
            new CurrencyCode(request.CurrencyCode)
        );

        await _accountRepository.AddAsync(account);
        await _unitOfWork.CommitAsync();
    }
}
