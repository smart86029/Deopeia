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
        var builder = new SqlBuilder();
        if (Guid.TryParse(request.IdOrSymbol, out var id))
        {
            builder.Where("a.id = @Id", new { id });
        }
        else
        {
            builder.Where("a.symbol = @Symbol", new { Symbol = request.IdOrSymbol });
        }

        var sql = builder.AddTemplate(
            """
SELECT
    a.id,
    a.symbol,
    COALESCE(b.name, c.name) AS name,
    currency_code
FROM instrument AS a
LEFT JOIN instrument_locale AS b ON a.id = b.instrument_id AND b.culture = @CurrentCulture
INNER JOIN instrument_locale AS c ON a.id = c.instrument_id AND c.culture = @DefaultThreadCurrentCulture
/**where**/
""",
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );
        var result = await _connection.QuerySingleAsync<GetInstrumentViewModel>(
            sql.RawSql,
            sql.Parameters
        );

        return result;
    }
}
