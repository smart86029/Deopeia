using Deopeia.Trading.Application.Traders.GetTraders;

namespace Deopeia.Trading.Infrastructure.Traders.GetTraders;

public class GetTradersQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetTradersQuery, PageResult<TraderDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<TraderDto>> Handle(
        GetTradersQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        if (request.IsEnabled.HasValue)
        {
            builder.Where("is_enabled = @IsEnabled", new { request.IsEnabled });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM trader /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<TraderDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.*,
    SUM(b.balance * c.exchange_rate) AS balance
FROM (
    SELECT
        id,
        name,
        is_enabled
    FROM trader
    /**where**/
    ORDER BY id
    LIMIT @Limit
    OFFSET @Offset
) AS a
INNER JOIN account AS b ON a.id = b.trader_id
INNER JOIN currency AS c ON b.currency_code = c.code
GROUP BY a.id, a.name, a.is_enabled
""",
            new
            {
                CultureInfo.CurrentCulture,
                CultureInfo.DefaultThreadCurrentCulture,
                result.Limit,
                result.Offset,
            }
        );
        var traders = await _connection.QueryAsync<TraderDto>(sql.RawSql, sql.Parameters);
        result.Items = traders.ToList();

        return result;
    }
}
