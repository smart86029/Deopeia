using Deopeia.Trading.Domain.Contracts;
using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Traders.Like;

public class LikeCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IContractRepository contractRepository,
    ITraderRepository traderRepository
) : IRequestHandler<LikeCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IContractRepository _contractRepository = contractRepository;
    private readonly ITraderRepository _traderRepository = traderRepository;

    public async Task Handle(LikeCommand request, CancellationToken cancellationToken)
    {
        var trader = await _traderRepository.GetTraderAsync(new TraderId(request.Id));
        var symbol = new Symbol(request.Symbol);
        if (await _contractRepository.ExistsAsync(symbol))
        {
            trader.Like(symbol);
        }

        await _unitOfWork.CommitAsync();
    }
}
