using Viriplaca.HR.App.Departments.GetDepartments;

namespace Viriplaca.HR.Data.Departments.GetDepartments;

internal class GetDepartmentsQueryHandler(SqlConnection connection)
    : IRequestHandler<GetDepartmentsQuery, PageResult<DepartmentDto>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<PageResult<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var builder = new SqlBuilder();
        if (request.IsEnabled.HasValue)
        {
            builder.Where("A.IsEnabled = @IsEnabled", new { request.IsEnabled });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM HR.Department AS A /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<DepartmentDto>(request, count);

        if (count == 0)
        {
            return result;
        }

        var sql = builder.AddTemplate(@"
SELECT
    A.Id,
    A.Name,
    A.IsEnabled,
    A.ParentId,
    D.FirstName,
    D.LastName,
    B.EmployeeCount
FROM HR.Department AS A
LEFT JOIN (
    SELECT DepartmentId, COUNT(*) AS EmployeeCount
    FROM HR.JobChange
    WHERE EndedAt IS NULL OR EndedAt >= GETDATE()
    GROUP BY DepartmentId
) AS B ON A.Id = B.DepartmentId
LEFT JOIN (
    SELECT DepartmentId, EmployeeId
    FROM HR.JobChange
    WHERE (EndedAt IS NULL OR EndedAt >= GETDATE()) AND IsHead = 1
) AS C ON A.Id = C.DepartmentId
LEFT JOIN (
    SELECT Id, FirstName, LastName
    FROM HR.Person
) AS D ON C.EmployeeId = D.Id
/**where**/
ORDER BY Id
OFFSET @Offset ROWS 
FETCH NEXT @Limit ROWS ONLY
");
        builder.AddParameters(new { result.Limit, result.Offset });
        var departments = await _connection.QueryAsync<DepartmentDto>(sql.RawSql, sql.Parameters);
        result.Items = departments.ToList();

        return result;
    }
}
