using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.OrderBooks;
using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Application.Orders.MockOrders;

internal class MockOrdersCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IAccountRepository accountRepository,
    IOrderBookRepository orderBookRepository,
    IEventBus eventBus
) : IRequestHandler<MockOrdersCommand>
{
    private static decimal price = 100M;

    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IOrderBookRepository _orderBookRepository = orderBookRepository;
    private readonly IEventBus _eventBus = eventBus;

    public async Task Handle(MockOrdersCommand request, CancellationToken cancellationToken)
    {
        var symbol = new Symbol("XAU");
        var currencyCode = new CurrencyCode("USD");

        var orderBook = await _orderBookRepository.GetOrderBookAsync(symbol);
        var accounts = await _accountRepository.GetAccountsAsync();

        var ramdom = new Random();
        foreach (var account in accounts.Take(4))
        {
            var offset = ramdom.Next(-5, 5);
            var abs = Math.Abs(offset);
            orderBook.AddOrder(
                (OrderSide)ramdom.Next(0, 2),
                ramdom.Next(abs * 2, abs * 10),
                (price + offset).ToMoney(currencyCode),
                null,
                null,
                account.Id
            );
        }
        await _unitOfWork.CommitAsync();

        foreach (var @event in orderBook.DomainEvents)
        {
            await _eventBus.PublishAsync(@event);
            if (@event is PriceChangedEvent priceChangedEvent)
            {
                price = priceChangedEvent.LastTradedPrice;
            }
        }

        orderBook.ClearDomainEvents();
    }
}
