using Viriplaca.HR.App.Leaves.GetLeaves;

namespace Viriplaca.HR.Data.Leaves.GetLeaves;

public class GetLeavesQueryHandler(SqlConnection connection)
    : IRequestHandler<GetLeavesQuery, PageResult<LeaveDto>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<PageResult<LeaveDto>> Handle(GetLeavesQuery request, CancellationToken cancellationToken)
    {
        var builder = new SqlBuilder();
        builder.Where("StartedAt <= @EndedAt", new { request.EndedAt });
        builder.Where("EndedAt >= @StartedAt", new { request.StartedAt });
        if (request.ApprovalStatus.HasValue)
        {
            builder.Where("ApprovalStatus = @ApprovalStatus", new { request.ApprovalStatus });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM HR.Leave /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = request.ToResult(count);

        if (count == 0)
        {
            return result;
        }

        var sql = builder.AddTemplate(@"
SELECT
    Id,
    Type,
    StartedAt,
    EndedAt,
    ApprovalStatus
FROM HR.Leave
/**where**/
ORDER BY Id
OFFSET @Offset ROWS
FETCH NEXT @Limit ROWS ONLY
");
        builder.AddParameters(new { result.Limit, result.Offset });
        var leaves = await _connection.QueryAsync<LeaveDto>(sql.RawSql, sql.Parameters);
        result.Items = leaves.ToList();

        return result;
    }
}
