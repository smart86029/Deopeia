using System.Text.RegularExpressions;
using Viriplaca.HR.App.LeaveEntitlements.GetLeaveEntitlements;

namespace Viriplaca.HR.Data.LeaveEntitlements.GetLeaveEntitlements;

internal class GetLeaveEntitlementsQueryHandler(SqlConnection connection)
    : IRequestHandler<GetLeaveEntitlementsQuery, ICollection<LeaveEntitlementDto>>
{
    private readonly SqlConnection _connection = connection;

    static GetLeaveEntitlementsQueryHandler()
    {
        SqlMapper.AddTypeHandler(new WorkingTimeTypeHandler());
    }

    public async Task<ICollection<LeaveEntitlementDto>> Handle(GetLeaveEntitlementsQuery request, CancellationToken cancellationToken)
    {
        var builder = new SqlBuilder();
        //builder.Where("EmployeeId = @EmployeeId", new { request.EmployeeId });
        builder.Where("A.StartedOn <= @Date AND A.EndedOn >= @Date", new { request.Date });

        var sql = builder.AddTemplate(@"
SELECT
    A.Id,
    A.StartedOn,
    A.EndedOn,
    A.GrantedTime,
    A.UsedTime,
    B.LeaveTypeId AS Id,
    B.Name,
    B.Description
FROM HR.LeaveEntitlement AS A
INNER JOIN HR.LeaveTypeLocale AS B ON A.LeaveTypeId = B.LeaveTypeId AND B.Culture = @Culture
/**where**/
", new { Culture = CultureInfo.CurrentCulture });
        var leaveEntitlements = await _connection.QueryAsync<LeaveEntitlementDto, LeaveTypeDto, LeaveEntitlementDto>(
            sql.RawSql,
            (leaveEntitlement, leaveType) =>
            {
                leaveEntitlement.LeaveType = leaveType;
                return leaveEntitlement;
            },
            sql.Parameters);

        return leaveEntitlements.ToList();
    }
}
