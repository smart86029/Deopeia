using Viriplaca.HR.App.Employees.GetEmployee;

namespace Viriplaca.HR.Data.Employees.GetEmployee;

public class GetEmployeeQueryHandler(SqlConnection connection)
    : IRequestHandler<GetEmployeeQuery, EmployeeDto>
{
    private readonly SqlConnection _connection = connection;

    public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var sql = @"
SELECT
    Id,
    FirstName,
    LastName,
    BirthDate,
    Sex,
    MaritalStatus,
    UserId
FROM HR.Person
WHERE Id = @Id AND Discriminator = N'Employee'
";
        var result = await _connection.QueryFirstAsync<EmployeeDto>(sql, request);

        return result;
    }
}
