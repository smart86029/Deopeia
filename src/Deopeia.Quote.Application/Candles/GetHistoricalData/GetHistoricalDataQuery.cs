namespace Deopeia.Quote.Application.Candles.GetHistoricalData;

public record GetHistoricalDataQuery(string IdOrSymbol) : IRequest<GetHistoricalDataViewModel> { }
