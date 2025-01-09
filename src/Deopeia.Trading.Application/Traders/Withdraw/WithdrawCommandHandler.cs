using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Traders.Withdraw;

public class WithdrawCommandHandler(
    ITradingUnitOfWork unitOfWork,
    ITraderRepository traderRepository
) : IRequestHandler<WithdrawCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITraderRepository _traderRepository = traderRepository;

    public async Task Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var trader = await _traderRepository.GetTraderAsync(new TraderId(request.Id));
        var money = new Money(new CurrencyCode(request.CurrencyCode), request.Amount);
        trader.Withdraw(money);

        await _unitOfWork.CommitAsync();
    }
}
