using Viriplaca.HR.App.Departments.GetDepartment;

namespace Viriplaca.HR.Data.Departments.GetDepartment;

internal class GetDepartmentQueryHandler(SqlConnection connection)
    : IRequestHandler<GetDepartmentQuery, DepartmentDto>
{
    private readonly SqlConnection _connection = connection;

    public async Task<DepartmentDto> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var sql = @"
SELECT
    Id,
    Name,
    IsEnabled,
    ParentId
FROM HR.Department
WHERE Id = @Id
";
        var result = await _connection.QueryFirstAsync<DepartmentDto>(sql, request);

        return result;
    }
}
