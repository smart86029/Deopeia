using Deopeia.Trading.Domain.Positions;
using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Positions.OpenPosition;

internal class OpenPositionCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IPositionRepository positionRepository
) : IRequestHandler<OpenPositionCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPositionRepository _positionRepository = positionRepository;

    public async Task Handle(OpenPositionCommand request, CancellationToken cancellationToken)
    {
        var instrumentId = new Symbol(request.Symbol);
        var openedBy = new TraderId(request.TraderId);
        var currencyCode = new CurrencyCode(request.CurrencyCode);
        var position = new Position(
            request.Type,
            instrumentId,
            request.Volume,
            request.Price?.ToMoney(currencyCode),
            request.StopLossPrice?.ToMoney(currencyCode),
            request.TakeProfitPrice?.ToMoney(currencyCode),
            openedBy
        );

        await _positionRepository.AddAsync(position);
        await _unitOfWork.CommitAsync();
    }
}
