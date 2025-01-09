using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Traders.UpdateTrader;

public class UpdateTraderCommandHandler(
    ITradingUnitOfWork unitOfWork,
    ITraderRepository traderRepository
) : IRequestHandler<UpdateTraderCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITraderRepository _traderRepository = traderRepository;

    public async Task Handle(UpdateTraderCommand request, CancellationToken cancellationToken)
    {
        var trader = await _traderRepository.GetTraderAsync(new TraderId(request.Id));
        trader.UpdateName(request.Name);

        if (request.IsEnabled)
        {
            trader.Enable();
        }
        else
        {
            trader.Disable();
        }

        await _unitOfWork.CommitAsync();
    }
}
