using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Traders.TraderCreated;

public class TraderCreatedEventHandler(
    ITradingUnitOfWork unitOfWork,
    ITraderRepository traderRepository
) : IEventHandler<TraderCreatedEvent>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITraderRepository _traderRepository = traderRepository;

    public async Task Handle(TraderCreatedEvent @event)
    {
        var trader = new Trader(@event.Id, @event.UserName);
        await _traderRepository.AddAsync(trader);
        await _unitOfWork.CommitAsync();
    }
}
