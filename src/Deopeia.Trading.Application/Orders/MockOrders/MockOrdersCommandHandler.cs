using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Contracts;
using Deopeia.Trading.Domain.OrderBooks;
using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Application.Orders.MockOrders;

internal class MockOrdersCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IAccountRepository accountRepository,
    IContractRepository contractRepository,
    IOrderBookRepository orderBookRepository,
    IEventBus eventBus
) : IRequestHandler<MockOrdersCommand>
{
    private static decimal price = 100M;

    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IContractRepository _contractRepository = contractRepository;
    private readonly IOrderBookRepository _orderBookRepository = orderBookRepository;
    private readonly IEventBus _eventBus = eventBus;

    public async Task Handle(MockOrdersCommand request, CancellationToken cancellationToken)
    {
        var currencyCode = new CurrencyCode("USD");
        var accounts = await _accountRepository.GetAccountsAsync();
        var contracts = await _contractRepository.GetContractsAsync();

        var ramdom = new Random();
        foreach (var contract in contracts)
        {
            var orderBook = await _orderBookRepository.GetOrderBookAsync(contract.Id);
            var offset = ramdom.Next(-5, 5);
            var abs = Math.Abs(offset);
            orderBook.AddOrder(
                (OrderSide)ramdom.Next(0, 2),
                ramdom.Next(abs * 2, abs * 10),
                (price + offset).ToMoney(currencyCode),
                null,
                null,
                accounts[ramdom.Next(0, 10)].Id
            );

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
}
