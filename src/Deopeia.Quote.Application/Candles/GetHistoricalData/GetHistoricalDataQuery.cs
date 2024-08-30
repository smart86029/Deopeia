namespace Deopeia.Quote.Application.Candles.GetHistoricalData;

public record GetHistoricalDataQuery(string Symbol) : IRequest<GetHistoricalDataViewModel> { }
