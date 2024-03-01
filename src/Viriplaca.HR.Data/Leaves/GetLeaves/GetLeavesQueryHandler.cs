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
        var result = new PageResult<LeaveDto>(request, count);

        if (count == 0)
        {
            return result;
        }

        var sql = $@"
SELECT
    Id,
    Type,
    StartedAt,
    EndedAt,
    ApprovalStatus
FROM HR.Leave
/**where**/
ORDER BY Id
LIMIT @Limit
OFFSET @Offset
";
        builder.AddParameters(new { result.Limit, result.Offset });
        var leaves = await _connection.QueryAsync<LeaveDto>(sql, request);
        result.Items = leaves.ToList();

        return result;
    }
}
