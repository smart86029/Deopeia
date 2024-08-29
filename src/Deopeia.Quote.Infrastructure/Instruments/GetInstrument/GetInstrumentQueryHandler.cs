using Deopeia.Quote.Application.Instruments.GetInstrument;

namespace Deopeia.Quote.Infrastructure.Instruments.GetInstrument;

internal class GetInstrumentQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetInstrumentQuery, GetInstrumentViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetInstrumentViewModel> Handle(
        GetInstrumentQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.symbol,
    b.name
FROM instrument AS a
INNER JOIN instrument_locale AS b ON a.id = b.instrument_id AND b.culture = @Culture
WHERE a.symbol = @Symbol
""";
        var result = await _connection.QuerySingleAsync<GetInstrumentViewModel>(
            sql,
            new { request.Symbol, Culture = CultureInfo.CurrentCulture }
        );

        return result;
    }
}
