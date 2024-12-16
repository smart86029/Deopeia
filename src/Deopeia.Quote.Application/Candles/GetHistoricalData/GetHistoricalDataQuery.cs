using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Application.Candles.GetHistoricalData;

public record GetHistoricalDataQuery(string Symbol, TimeFrame TimeFrame, DateTimeOffset? StartedAt)
    : IRequest<List<CandleDto>> { }
