using Viriplaca.HR.App.Employees.GetEmployees;
using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.Data.Employees.GetEmployees;

public class GetEmployeesQueryHandler(SqlConnection connection)
    : IRequestHandler<GetEmployeesQuery, PageResult<EmployeeDto>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<PageResult<EmployeeDto>> Handle(
        GetEmployeesQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        builder.Where("A.Type = @Employee", new { PersonType.Employee });
        if (request.DepartmentId.HasValue)
        {
            builder.Where("A.DepartmentId = @DepartmentId", new { request.DepartmentId });
        }

        if (request.JobId.HasValue)
        {
            builder.Where("A.JobId = @JobId", new { request.JobId });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM HR.Person AS A /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = request.ToResult(count);

        if (count == 0)
        {
            return result;
        }

        var sql = builder.AddTemplate(
            $@"
SELECT
    A.Id,
    A.FirstName,
    A.LastName,
    A.UserId,
    B.Name AS DepartmentName,
    C.Title AS JobTitle
FROM HR.Person AS A
LEFT JOIN HR.Department AS B ON B.Id = A.DepartmentId
LEFT JOIN HR.Job AS C ON C.Id = A.JobId
/**where**/
ORDER BY A.Id
OFFSET @Offset ROWS
FETCH NEXT @Limit ROWS ONLY
"
        );
        builder.AddParameters(new { result.Limit, result.Offset });
        var employees = await _connection.QueryAsync<EmployeeDto>(sql.RawSql, sql.Parameters);
        result.Items = employees.ToList();

        return result;
    }
}
