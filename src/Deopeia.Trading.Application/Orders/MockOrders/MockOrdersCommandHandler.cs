using Deopeia.Trading.Domain.Contracts;
using Deopeia.Trading.Domain.OrderBooks;
using Deopeia.Trading.Domain.Orders;
using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Application.Orders.MockOrders;

internal class MockOrdersCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IContractRepository contractRepository,
    IOrderBookRepository orderBookRepository,
    ITraderRepository traderRepository,
    IEventProducer eventProducer
) : IRequestHandler<MockOrdersCommand>
{
    private static readonly CurrencyCode Usd = new("USD");
    private static readonly Dictionary<Symbol, decimal> Prices = new()
    {
        [new Symbol("AAPL")] = 243,
        [new Symbol("DJI")] = 42732,
        [new Symbol("NDX")] = 21326,
        [new Symbol("SPX")] = 5942,
        [new Symbol("XAU")] = 2640,
        [new Symbol("XAG")] = 141,
        [new Symbol("EURUSD")] = 1.0336M,
        [new Symbol("USDJPY")] = 157.75M,
        [new Symbol("GBPUSD")] = 1.24M,
        [new Symbol("AUDUSD")] = 0.624M,
        [new Symbol("BTC")] = 43.60M,
        [new Symbol("ETH")] = 33.97M,
    };

    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;

    private readonly IContractRepository _contractRepository = contractRepository;
    private readonly IOrderBookRepository _orderBookRepository = orderBookRepository;
    private readonly ITraderRepository _traderRepository = traderRepository;
    private readonly IEventProducer _eventProducer = eventProducer;

    public async Task Handle(MockOrdersCommand request, CancellationToken cancellationToken)
    {
        var contracts = await _contractRepository.GetContractsAsync();
        var traders = await _traderRepository.GetTradersAsync();
        var tradersCount = traders.Count;
        if (tradersCount == 0)
        {
            return;
        }

        var ramdom = new Random();
        foreach (var contract in contracts)
        {
            var orderBook = await _orderBookRepository.GetOrderBookAsync(contract.Id);
            foreach (var side in Enum.GetValues<OrderSide>())
            {
                var takerPrice = side == OrderSide.Buy ? orderBook.Bid : orderBook.Ask;
                if (takerPrice == 0)
                {
                    takerPrice = Prices[contract.Id];
                }

                var makerPrice = side == OrderSide.Buy ? orderBook.Ask : orderBook.Bid;
                if (makerPrice == 0)
                {
                    makerPrice = Prices[contract.Id];
                }

                var (openPrice, volume) = GetRandomPriceAndVolume(
                    side,
                    contract,
                    (takerPrice + makerPrice) / 2
                );
                orderBook.AddOrder(
                    side,
                    volume,
                    openPrice,
                    null,
                    null,
                    traders[ramdom.Next(0, tradersCount)].Id
                );

                await _unitOfWork.CommitAsync();

                foreach (var @event in orderBook.DomainEvents)
                {
                    await _eventProducer.ProduceAsync(@event);
                }

                orderBook.ClearDomainEvents();
            }
        }

        (Money Price, decimal Volume) GetRandomPriceAndVolume(
            OrderSide side,
            Contract contract,
            decimal price
        )
        {
            var next = side == OrderSide.Buy ? ramdom.Next(1, 11) : ramdom.Next(-10, 0);
            var offset = next * contract.TickSize;
            var amount = price + offset;
            var money = Math.Round(amount, 2).ToMoney(Usd);

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
