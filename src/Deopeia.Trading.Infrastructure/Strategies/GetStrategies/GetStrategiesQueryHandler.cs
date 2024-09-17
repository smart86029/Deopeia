using Deopeia.Trading.Application.Strategies.GetStrategies;

namespace Deopeia.Trading.Infrastructure.Strategies.GetStrategies;

public class GetStrategiesQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetStrategiesQuery, PageResult<StrategyDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<StrategyDto>> Handle(
        GetStrategiesQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM strategy AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<StrategyDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.id,
    a.is_enabled,
    COALESCE(b.name, c.name) AS name,
    COALESCE(b.description, c.description) AS description
FROM strategy AS a
LEFT JOIN strategy_locale AS b ON a.id = b.strategy_id AND b.culture = @CurrentCulture
INNER JOIN strategy_locale AS c ON a.id = c.strategy_id AND c.culture = @DefaultThreadCurrentCulture
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
        var strategys = await _connection.QueryAsync<StrategyDto>(sql.RawSql, sql.Parameters);
        result.Items = strategys.ToList();

        return result;
    }
}
