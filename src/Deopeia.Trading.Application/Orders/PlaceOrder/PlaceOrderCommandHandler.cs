using Deopeia.Trading.Domain.OrderBooks;
using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Orders.PlaceOrder;

internal class PlaceOrderCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IOrderBookRepository orderBookRepository,
    IEventProducer eventProducer
) : IRequestHandler<PlaceOrderCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderBookRepository _orderBookRepository = orderBookRepository;
    private readonly IEventProducer _eventProducer = eventProducer;

    public async Task Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var symbol = new Symbol(request.Symbol);
        var orderBook = await _orderBookRepository.GetOrderBookAsync(symbol);

        var openedBy = new TraderId(request.AccountId);
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
            await _eventProducer.ProduceAsync(@event);
        }

        orderBook.ClearDomainEvents();
    }
}
