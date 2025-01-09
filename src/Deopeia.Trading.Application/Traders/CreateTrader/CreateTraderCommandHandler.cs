using Deopeia.Trading.Application.Traders.CreateTrader;
using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Accounts.CreateAccount;

public class CreateTraderCommandHandler(
    ITradingUnitOfWork unitOfWork,
    ITraderRepository traderRepository
) : IRequestHandler<CreateTraderCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITraderRepository _traderRepository = traderRepository;

    public async Task Handle(CreateTraderCommand request, CancellationToken cancellationToken)
    {
        var trader = new Trader(request.Name, request.IsEnabled);

        await _traderRepository.AddAsync(trader);
        await _unitOfWork.CommitAsync();
    }
}
