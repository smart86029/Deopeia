using Viriplaca.HR.App.Leaves;
using Viriplaca.HR.App.Leaves.GetLeaves;
using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.Data.Leaves.GetLeaves;

public class GetLeavesQueryHandler(SqlConnection connection)
    : IRequestHandler<GetLeavesQuery, PageResult<LeaveDto>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<PageResult<LeaveDto>> Handle(GetLeavesQuery request, CancellationToken cancellationToken)
    {
        var builder = new SqlBuilder();
        builder.Where("A.StartedAt <= @EndedAt", new { request.EndedAt });
        builder.Where("A.EndedAt >= @StartedAt", new { request.StartedAt });
        if (request.ApprovalStatus.HasValue)
        {
            builder.Where("A.ApprovalStatus = @ApprovalStatus", new { request.ApprovalStatus });
        }
        if (request.EmployeeId.HasValue)
        {
            builder.Where("A.EmployeeId = @EmployeeId", new { request.EmployeeId });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM HR.Leave AS A /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = request.ToResult(count);

        if (count == 0)
        {
            return result;
        }

        var sql = builder.AddTemplate(@"
SELECT
    A.Id,
    A.Type,
    A.StartedAt,
    A.EndedAt,
    A.ApprovalStatus,
    B.Id,
    B.FirstName,
    B.LastName
FROM HR.Leave AS A
INNER JOIN HR.Person AS B ON A.EmployeeId = B.Id AND B.Type = @Employee
/**where**/
ORDER BY A.Id DESC
OFFSET @Offset ROWS
FETCH NEXT @Limit ROWS ONLY
");
        builder.AddParameters(new { PersonType.Employee, result.Limit, result.Offset });
        var leaves = await _connection.QueryAsync<LeaveDto, EmployeeDto, LeaveDto>(
            sql.RawSql,
            (leave, employee) =>
            {
                leave.Employee = employee;
                return leave;
            },
            sql.Parameters);
        result.Items = leaves.ToList();

        return result;
    }
}
