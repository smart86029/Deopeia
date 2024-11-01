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
        _lastTradePrice += new Random().Next(-10, 10);
        var map = await _instrumentRepository.GetSymbolMapAsync(new ExchangeId("XCEC"));
        var instrumentId = map["GCZ2024"];
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

        var @event = new PriceChangedEvent("GCZ2024", DateTimeOffset.UtcNow, _lastTradePrice, 2500);
        await _eventBus.PublishAsync(@event);
    }
}
