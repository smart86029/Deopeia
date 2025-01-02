using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Application.Candles.DealCreated;

public class DealCreatedEventHandler(
    IQuoteUnitOfWork unitOfWork,
    ICandleRepository candleRepository
) : IEventHandler<DealCreatedEvent>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICandleRepository _candleRepository = candleRepository;

    public async Task Handle(DealCreatedEvent @event)
    {
        var symbol = new Symbol(@event.Symbol);
        var tick = new Tick(symbol, @event.CreatedAt, @event.Price, @event.Volume);
        await _candleRepository.AddAsync(symbol, tick);
        await _unitOfWork.CommitAsync();
    }
}
