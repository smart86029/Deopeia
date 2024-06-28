namespace Deopeia.Quote.Application.Ohlcvs.GetHistoricalData;

public record GetHistoricalDataQuery(string Symbol) : IRequest<GetHistoricalDataViewModel> { }
