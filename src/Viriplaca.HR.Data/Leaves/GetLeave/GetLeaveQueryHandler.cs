using Viriplaca.HR.App.Leaves;
using Viriplaca.HR.App.Leaves.GetLeave;
using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.Data.Leaves.GetLeave;

public class GetLeaveQueryHandler(SqlConnection connection)
    : IRequestHandler<GetLeaveQuery, LeaveDto>
{
    private readonly SqlConnection _connection = connection;

    public async Task<LeaveDto> Handle(GetLeaveQuery request, CancellationToken cancellationToken)
    {
        var sql =
            @"
SELECT
    A.Id,
    A.LeaveTypeId,
    A.StartedAt,
    A.EndedAt,
    A.Reason,
    A.ApprovalStatus,
    B.Id,
    B.FirstName,
    B.LastName
FROM HR.Leave AS A
INNER JOIN HR.Person AS B ON A.EmployeeId = B.Id AND B.Type = @Employee
WHERE A.Id = @Id
";
        var param = new { request.Id, PersonType.Employee, };
        var leaves = await _connection.QueryAsync<LeaveDto, EmployeeDto, LeaveDto>(
            sql,
            (leave, employee) =>
            {
                leave.Employee = employee;
                return leave;
            },
            param
        );

        return leaves.First();
    }
}
