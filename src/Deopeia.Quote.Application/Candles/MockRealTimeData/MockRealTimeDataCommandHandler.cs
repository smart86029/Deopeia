using Deopeia.Quote.Application.Candles.ScrapeRealTimeData;
using Deopeia.Quote.Domain.ContractSpecifications;
using Deopeia.Quote.Domain.Instruments.FuturesContracts;

namespace Deopeia.Quote.Application.Candles.MockRealTimeData;

internal class MockRealTimeDataCommandHandler(
    IContractSpecificationRepository contractSpecificationRepository,
    IFuturesContractRepository futuresContractRepository,
    IEventBus eventBus
) : IRequestHandler<MockRealTimeDataCommand>
{
    private readonly IContractSpecificationRepository _contractSpecificationRepository =
        contractSpecificationRepository;
    private readonly IFuturesContractRepository _futuresContractRepository =
        futuresContractRepository;
    private readonly IEventBus _eventBus = eventBus;

    private decimal _lastTradePrice = 2500;

    public async Task Handle(MockRealTimeDataCommand request, CancellationToken cancellationToken)
    {
        _lastTradePrice += new Random().Next(-10, 10);
        var @event = new PriceChangedEvent("GCZ2024", DateTimeOffset.UtcNow, _lastTradePrice, 2500);
        await _eventBus.PublishAsync(@event);
    }
}
