using System.Security.Cryptography;
using System.Threading.Tasks;
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
            var price = 0M;
            var orderBook = await _orderBookRepository.GetOrderBookAsync(contract.Id);
            foreach (var side in Enum.GetValues<OrderSide>())
            {
                var takerPrice = side == OrderSide.Buy ? orderBook.Bid : orderBook.Ask;
                if (takerPrice == price)
                {
                    continue;
                }

                var makerPrice = side == OrderSide.Buy ? orderBook.Ask : orderBook.Bid;
                var (openPrice, volume) = GetRandomPriceAndVolume(
                    side,
                    contract.TickSize,
                    price,
                    makerPrice
                );
                orderBook.AddOrder(
                    side,
                    volume,
                    openPrice,
                    null,
                    null,
                    accounts[ramdom.Next(0, 10)].Id
                );

                await _unitOfWork.CommitAsync();

                foreach (var @event in orderBook.DomainEvents)
                {
                    await _eventBus.PublishAsync(@event);
                    if (@event is DealCreatedEvent priceChangedEvent)
                    {
                        price = priceChangedEvent.Price;
                    }
                }

                orderBook.ClearDomainEvents();
            }
        }

        (Money Price, decimal Volume) GetRandomPriceAndVolume(
            OrderSide side,
            decimal tickSize,
            decimal price,
            decimal makerPrice
        )
        {
            var next = side == OrderSide.Buy ? ramdom.Next(0, 11) : ramdom.Next(-10, 1);
            var offset = makerPrice == price ? 0 : next * tickSize;
            var amount = makerPrice + offset;
            var money = Math.Round(amount, 2).ToMoney(currencyCode);

            var volumeMean = 4.0;
            var volumeStdDev = 1.0;
            var u1 = 1.0 - ramdom.NextDouble();
            var u2 = Math.Abs(next * 0.01);
            var normalRandom = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
            var volume = Math.Exp(volumeMean + volumeStdDev * normalRandom).ToInt();

            return (money, volume);
        }
    }
}
