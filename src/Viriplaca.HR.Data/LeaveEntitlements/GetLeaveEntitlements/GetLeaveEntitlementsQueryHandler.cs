using Viriplaca.HR.App.LeaveEntitlements.GetLeaveEntitlements;

namespace Viriplaca.HR.Data.LeaveEntitlements.GetLeaveEntitlements;

internal class GetLeaveEntitlementsQueryHandler(SqlConnection connection)
    : IRequestHandler<GetLeaveEntitlementsQuery, ICollection<LeaveEntitlementDto>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<ICollection<LeaveEntitlementDto>> Handle(GetLeaveEntitlementsQuery request, CancellationToken cancellationToken)
    {
        var builder = new SqlBuilder();
        //builder.Where("EmployeeId = @EmployeeId", new { request.EmployeeId });
        builder.Where("StartedOn <= @Date AND EndedOn >= @Date", new { request.Date });

        var sql = builder.AddTemplate(@"
SELECT
    Id,
    Type,
    StartedOn,
    EndedOn,
    AvailableHours,
    UsedHours
FROM HR.LeaveEntitlement
/**where**/
");
        var leaveEntitlements = await _connection.QueryAsync<LeaveEntitlementDto>(sql.RawSql, sql.Parameters);

        return leaveEntitlements.ToList();
    }
}
