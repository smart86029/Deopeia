using Deopeia.Trading.Application.Positions.GetPositions;

namespace Deopeia.Trading.Infrastructure.Positions.GetPositions;

public class GetPositionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetPositionsQuery, PageResult<PositionDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<PositionDto>> Handle(
        GetPositionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        if (request.OpenedBy.HasValue)
        {
            builder.Where("a.opened_by = @OpenedBy", new { request.OpenedBy });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM position AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<PositionDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.id,
    d.account_number,
    a.instrument_id,
    a.side,
    a.volume,
    COALESCE(b.name, c.name) AS currency,
    a.margin_amount AS margin,
    a.open_price_amount AS open_price,
    a.opened_at
FROM position AS a
LEFT JOIN currency_locale AS b
    ON a.margin_currency_code = b.currency_code AND b.culture = @CurrentCulture
INNER JOIN currency_locale AS c
    ON a.margin_currency_code = c.currency_code AND c.culture = @DefaultThreadCurrentCulture
INNER JOIN account AS d ON a.opened_by = d.id
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
        var positions = await _connection.QueryAsync<PositionDto>(sql.RawSql, sql.Parameters);
        result.Items = positions.ToList();

        return result;
    }
}
