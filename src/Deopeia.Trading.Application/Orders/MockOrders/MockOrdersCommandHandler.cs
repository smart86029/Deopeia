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
    private static decimal price = 1000M;

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
            for (var i = 0; i < 2; i++)
            {
                var orderBook = await _orderBookRepository.GetOrderBookAsync(contract.Id);
                var side = (OrderSide)i;
                orderBook.AddOrder(
                    side,
                    GetVolume(),
                    GetPrice(side),
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
                        if (priceChangedEvent.Bid == 0 || priceChangedEvent.Ask == 0)
                        {
                            if (priceChangedEvent.Price > 0)
                            {
                                price = priceChangedEvent.Price;
                            }

                            continue;
                        }

                        price = (priceChangedEvent.Bid + priceChangedEvent.Ask) / 2;
                    }
                }

                orderBook.ClearDomainEvents();
            }
        }

        decimal GetVolume()
        {
            var volumeMean = 4.0;
            var volumeStdDev = 1.0;
            var u1 = 1.0 - ramdom.NextDouble();
            var u2 = 1.0 - ramdom.NextDouble();
            var normalRandom = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
            var volume = Math.Exp(volumeMean + volumeStdDev * normalRandom).ToInt();

            return volume;
        }

        Money GetPrice(OrderSide side)
        {
            var offset = Math.Max(ramdom.NextDouble().ToDecimal(), 0.2M) * price;
            var amount = side == OrderSide.Buy ? price + offset : price - offset;

            return Math.Round(amount, 2).ToMoney(currencyCode);
        }
    }
}
