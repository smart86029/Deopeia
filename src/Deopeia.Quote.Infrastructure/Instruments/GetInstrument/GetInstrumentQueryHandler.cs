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
    COALESCE(b.name, c.name) AS name
FROM instrument AS a
LEFT JOIN instrument_locale AS b ON a.id = b.instrument_id AND b.culture = @CurrentCulture
INNER JOIN instrument_locale AS c ON a.id = c.instrument_id AND c.culture = @DefaultThreadCurrentCulture
WHERE a.symbol = @Symbol
""";
        var result = await _connection.QuerySingleAsync<GetInstrumentViewModel>(
            sql,
            new
            {
                CultureInfo.CurrentCulture,
                CultureInfo.DefaultThreadCurrentCulture,
                request.Symbol,
            }
        );

        return result;
    }
}
