using Deopeia.Quote.Application.Candles.ScrapeRealTimeData;
using Deopeia.Quote.Domain.Candles;
using Deopeia.Quote.Domain.ContractSpecifications;
using Deopeia.Quote.Domain.Instruments;
using Deopeia.Quote.Domain.Instruments.FuturesContracts;

namespace Deopeia.Quote.Application.Candles.MockRealTimeData;

internal class MockRealTimeDataCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IContractSpecificationRepository contractSpecificationRepository,
    IFuturesContractRepository futuresContractRepository,
    IInstrumentRepository instrumentRepository,
    ICandleRepository candleRepository,
    IEventBus eventBus
) : IRequestHandler<MockRealTimeDataCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IContractSpecificationRepository _contractSpecificationRepository =
        contractSpecificationRepository;
    private readonly IFuturesContractRepository _futuresContractRepository =
        futuresContractRepository;
    private readonly IInstrumentRepository _instrumentRepository = instrumentRepository;
    private readonly ICandleRepository _candleRepository = candleRepository;
    private readonly IEventBus _eventBus = eventBus;

    private decimal _lastTradePrice = 2500;

    public async Task Handle(MockRealTimeDataCommand request, CancellationToken cancellationToken)
    {
        var symbol = "GCZ2024";

        await MockPriceAsync(symbol);
        //await MockOrderBookAsync(symbol);
    }

    private async Task MockPriceAsync(string symbol)
    {
        var random = new Random();
        _lastTradePrice += random.Next(-10, 10);
        var map = await _instrumentRepository.GetSymbolMapAsync(new ExchangeId("XCEC"));
        var instrumentId = map[symbol];
        var candle = new Candle(
            instrumentId,
            TimeFrame.Intraday,
            DateTimeOffset.UtcNow,
            2500,
            _lastTradePrice,
            _lastTradePrice,
            _lastTradePrice,
            0
        );

        await _candleRepository.AddAsync(candle);
        await _unitOfWork.CommitAsync();

        var @event = new PriceChangedEvent(symbol, DateTimeOffset.UtcNow, _lastTradePrice, 2500);
        await _eventBus.PublishAsync(@event);
    }

    private async Task MockOrderBookAsync(string symbol)
    {
        var random = new Random();
        var spread = 1M;
        var baseBidPrice = Math.Round(_lastTradePrice - spread, 2);
        var baseAskPrice = Math.Round(_lastTradePrice + spread, 2);

        var bids = new OrderDto[5];
        var asks = new OrderDto[5];
        for (var i = 0; i < 5; i++)
        {
            var priceChange = i * 1M;
            var sizeMax = (i + 1) * 100;

            var bidPrice = Math.Round(baseBidPrice - priceChange, 2);
            var bidSizeMin = i == 0 ? 1 : bids[i - 1].Size;
            var bidSize = random.Next(bidSizeMin, sizeMax);
            bids[i] = new OrderDto { Price = bidPrice, Size = bidSize };

            var askPrice = Math.Round(baseAskPrice + priceChange, 2);
            var askSizeMin = i == 0 ? 1 : asks[i - 1].Size;
            var askSize = random.Next(askSizeMin, sizeMax);
            asks[i] = new OrderDto { Price = askPrice, Size = askSize };
        }

        await _eventBus.PublishAsync(new OrderBookChangedEvent(symbol, bids, asks));
    }
}
