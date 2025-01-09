using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Traders.Deposit;

public class DepositCommandHandler(
    ITradingUnitOfWork unitOfWork,
    ITraderRepository traderRepository
) : IRequestHandler<DepositCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITraderRepository _traderRepository = traderRepository;

    public async Task Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var trader = await _traderRepository.GetTraderAsync(new TraderId(request.Id));
        var money = new Money(new CurrencyCode(request.CurrencyCode), request.Amount);
        trader.Deposit(money);

        await _unitOfWork.CommitAsync();
    }
}
