using Deopeia.Quote.Application.Options.GetUnits;

namespace Deopeia.Quote.Infrastructure.Options.GetUnitsOfMeasurement;

internal class GetUnitsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetUnitsQuery, ICollection<OptionResult<string>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<string>>> Handle(
        GetUnitsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.code AS value,
    CONCAT(COALESCE(b.name, c.name), CASE WHEN a.symbol IS NOT NULL THEN CONCAT(' (', a.symbol, ')') END) AS name
FROM unit AS a
LEFT JOIN unit_locale AS b ON a.code = b.unit_code AND b.culture = @CurrentCulture
INNER JOIN unit_locale AS c ON a.code = c.unit_code AND c.culture = @DefaultThreadCurrentCulture
""";
        var optoins = await _connection.QueryAsync<OptionResult<string>>(
            sql,
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );

        return optoins.ToList();
    }
}
