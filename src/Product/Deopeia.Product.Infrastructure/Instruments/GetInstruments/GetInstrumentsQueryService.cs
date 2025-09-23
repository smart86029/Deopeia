using Deopeia.Product.Application.Instruments.GetInstruments;

namespace Deopeia.Product.Infrastructure.Instruments.GetInstruments;

internal sealed class GetInstrumentsQueryService(NpgsqlConnection connection)
    : IGetInstrumentsQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PagedResult<InstrumentDto>> GetAsync(GetInstrumentsQuery query)
    {
        var builder = new SqlBuilder();
        builder.LeftJoin(
            "instrument_localization AS b ON a.id = b.instrument_id AND b.culture = @CurrentCulture",
            new { CultureInfo.CurrentCulture }
        );
        builder.LeftJoin(
            "instrument_localization AS c ON a.id = c.instrument_id AND c.culture = @DefaultThreadCurrentCulture",
            new { CultureInfo.DefaultThreadCurrentCulture }
        );

        if (!query.Keyword.IsNullOrWhiteSpace())
        {
            builder.Where("a.symbol ILIKE @Keyword", new { Keyword = $"%{query.Keyword}%" });
            builder.OrWhere("b.name ILIKE @Keyword");
            builder.OrWhere("c.name ILIKE @Keyword");
        }

        var counterSql = "SELECT COUNT(*) FROM instrument AS a /**leftjoin**/ /**where**/";
        var selectorSql = """
SELECT
    a.id,
    a.symbol,
    COALESCE(b.name, c.name, a.symbol) AS name,
    a.type
FROM instrument AS a
/**leftjoin**/
/**where**/
ORDER BY a.symbol
/**pagination**/
""";
        return await _connection.QueryPagedResultAsync(builder, counterSql, selectorSql, query);
    }
}
