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
    id,
    name,
    is_enabled
FROM trader
/**where**/
ORDER BY id
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
        var traders = await _connection.QueryAsync<TraderDto>(sql.RawSql, sql.Parameters);
        result.Items = traders.ToList();

        return result;
    }
}
