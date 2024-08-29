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
    a.id,
    a.code,
    b.name,
    a.time_zone,
    a.opening_time,
    a.closing_time
FROM exchange AS a
INNER JOIN exchange_locale AS b ON a.id = b.exchange_id AND b.culture = @Culture
/**where**/
LIMIT @Limit
OFFSET @Offset
""",
            new
            {
                Culture = CultureInfo.CurrentCulture,
                result.Limit,
                result.Offset,
            }
        );
        var exchanges = await _connection.QueryAsync<ExchangeDto>(sql.RawSql, sql.Parameters);
        result.Items = exchanges.ToList();

        return result;
    }
}
