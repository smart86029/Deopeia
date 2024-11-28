using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.MatchingEngines;

namespace Deopeia.Trading.Application.Orders.PlaceOrder;

internal class PlaceOrderCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IOrderBookRepository orderBookRepository,
    IEventBus eventBus
) : IRequestHandler<PlaceOrderCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderBookRepository _orderBookRepository = orderBookRepository;
    private readonly IEventBus _eventBus = eventBus;

    public async Task Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var instrumentId = new InstrumentId(request.InstrumentId);
        var orderBook = await _orderBookRepository.GetOrderBookAsync(instrumentId);

        var openedBy = new AccountId(request.AccountId);
        var currencyCode = new CurrencyCode(request.CurrencyCode);
        orderBook.AddOrder(
            request.Side,
            request.Volume,
            request.Price?.ToMoney(currencyCode),
            request.StopLossPrice?.ToMoney(currencyCode),
            request.TakeProfitPrice?.ToMoney(currencyCode),
            openedBy
        );

        await _unitOfWork.CommitAsync();

        foreach (var @event in orderBook.DomainEvents)
        {
            await _eventBus.PublishAsync(@event);
        }

        orderBook.ClearDomainEvents();
    }
}
