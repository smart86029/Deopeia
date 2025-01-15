using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Traders.Dislike;

public class DislikeCommandHandler(
    ITradingUnitOfWork unitOfWork,
    ITraderRepository traderRepository
) : IRequestHandler<DislikeCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITraderRepository _traderRepository = traderRepository;

    public async Task Handle(DislikeCommand request, CancellationToken cancellationToken)
    {
        var trader = await _traderRepository.GetTraderAsync(new TraderId(request.Id));
        var symbol = new Symbol(request.Symbol);
        trader.Dislike(symbol);

        await _unitOfWork.CommitAsync();
    }
}
