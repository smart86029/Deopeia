using Deopeia.Quote.Application.Candles.GetHistoricalData;

namespace Deopeia.Quote.Infrastructure.Candles.GetHistoricalData;

internal class GetHistoricalDataQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetHistoricalDataQuery, GetHistoricalDataViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetHistoricalDataViewModel> Handle(
        GetHistoricalDataQuery request,
        CancellationToken cancellationToken
    )
    {
        if (!Guid.TryParse(request.IdOrSymbol, out var instrumentId))
        {
            instrumentId = await _connection.ExecuteScalarAsync<Guid>(
                "SELECT Id FROM instrument WHERE symbol = @Symbol",
                new { Symbol = request.IdOrSymbol }
            );
        }

        var sql = """
SELECT
    timestamp AS date,
    open,
    high,
    low,
    close,
    volume
FROM candle
WHERE instrument_id = @InstrumentId
ORDER BY timestamp
LIMIT 100
""";

        var quotes = await _connection.QueryAsync<CandleDto>(
            sql,
            new { InstrumentId = instrumentId }
        );
        var result = new GetHistoricalDataViewModel { Quotes = quotes.ToList() };

        return result;
    }
}
