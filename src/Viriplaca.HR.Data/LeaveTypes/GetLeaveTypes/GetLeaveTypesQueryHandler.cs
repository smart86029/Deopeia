using Viriplaca.HR.App.LeaveTypes.GetLeaveTypes;

namespace Viriplaca.HR.Data.LeaveTypes.GetLeaveTypes;

internal class GetLeaveTypesQueryHandler(SqlConnection connection)
    : IRequestHandler<GetLeaveTypesQuery, PageResult<LeaveTypeDto>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<PageResult<LeaveTypeDto>> Handle(
        GetLeaveTypesQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM HR.LeaveType AS A /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = request.ToResult(count);

        if (count == 0)
        {
            return result;
        }

        var sql = builder.AddTemplate(
            @"
SELECT
    A.Id,
    B.Name,
    B.Description,
    A.CanCarryForward
FROM HR.LeaveType AS A
LEFT JOIN HR.LeaveTypeLocale AS B ON A.Id = B.LeaveTypeId AND B.Culture = @Culture
/**where**/
ORDER BY A.Id
OFFSET @Offset ROWS
FETCH NEXT @Limit ROWS ONLY
",
            new
            {
                Culture = CultureInfo.CurrentCulture,
                result.Offset,
                result.Limit,
            }
        );
        var leaveTypes = await _connection.QueryAsync<LeaveTypeDto>(sql.RawSql, sql.Parameters);
        result.Items = leaveTypes.ToList();

        return result;
    }
}
