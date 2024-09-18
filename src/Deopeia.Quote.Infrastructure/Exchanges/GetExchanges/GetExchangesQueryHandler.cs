using Deopeia.Quote.Application.Exchanges.GetExchanges;

namespace Deopeia.Quote.Infrastructure.Exchanges.GetExchanges;

public class GetExchangesQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetExchangesQuery, PageResult<ExchangeDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<ExchangeDto>> Handle(
        GetExchangesQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM exchange AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<ExchangeDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.id AS mic,
    COALESCE(b.name, c.name) AS name,
    COALESCE(b.abbreviation, c.abbreviation) AS abbreviation,
    a.time_zone
FROM exchange AS a
LEFT JOIN exchange_locale AS b ON a.id = b.exchange_id AND b.culture = @CurrentCulture
INNER JOIN exchange_locale AS c ON a.id = c.exchange_id AND c.culture = @DefaultThreadCurrentCulture
/**where**/
LIMIT @Limit
OFFSET @Offset
""",
            new
            {
                CultureInfo.CurrentCulture,
                CultureInfo.DefaultThreadCurrentCulture,
                result.Limit,
                result.Offset,
            }
        );
        var exchanges = await _connection.QueryAsync<ExchangeDto>(sql.RawSql, sql.Parameters);
        foreach (var exchange in exchanges)
        {
            exchange.TimeZone = TimeZoneInfo
                .FindSystemTimeZoneById(exchange.TimeZone)
                .GetDisplayName();
        }

        result.Items = exchanges.ToList();

        return result;
    }
}
