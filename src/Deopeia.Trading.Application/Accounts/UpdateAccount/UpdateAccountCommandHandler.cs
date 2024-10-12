using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Application.Accounts.UpdateAccount;

public class UpdateAccountCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IAccountRepository accountRepository
) : IRequestHandler<UpdateAccountCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountAsync(new AccountId(request.Id));
        if (request.IsEnabled)
        {
            account.Enable();
        }
        else
        {
            account.Disable();
        }

        await _unitOfWork.CommitAsync();
    }
}
