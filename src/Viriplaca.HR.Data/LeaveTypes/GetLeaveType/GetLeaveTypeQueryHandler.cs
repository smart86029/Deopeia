using Viriplaca.HR.App.LeaveTypes;
using Viriplaca.HR.App.LeaveTypes.GetLeaveType;

namespace Viriplaca.HR.Data.LeaveTypes.GetLeaveType;

internal class GetLeaveTypeQueryHandler(SqlConnection connection)
    : IRequestHandler<GetLeaveTypeQuery, LeaveTypeDto>
{
    private readonly SqlConnection _connection = connection;

    public async Task<LeaveTypeDto> Handle(
        GetLeaveTypeQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql =
            @"
SELECT
    Id,
    CanCarryForward
FROM HR.LeaveType
WHERE Id = @Id
";
        var result = await _connection.QuerySingleAsync<LeaveTypeDto>(sql, request);

        sql =
            @"
SELECT
    Culture,
    Name,
    Description
FROM HR.LeaveTypeLocale
WHERE LeaveTypeId = @Id
";
        var leaveTypeLocales = await _connection.QueryAsync<LeaveTypeLocaleDto>(sql, request);
        result.Locales = leaveTypeLocales.ToList();

        return result;
    }
}
