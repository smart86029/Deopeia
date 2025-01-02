using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Application.Candles.CalculateCandles;

public record CalculateCandlesCommand(TimeFrame TimeFrame) : IRequest { }
