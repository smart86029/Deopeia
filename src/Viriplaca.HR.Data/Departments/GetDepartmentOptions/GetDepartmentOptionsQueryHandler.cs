namespace Viriplaca.HR.App.Departments.GetDepartmentOptions;

public class GetDepartmentOptionsQueryHandler(SqlConnection connection)
    : IRequestHandler<GetDepartmentOptionsQuery, ICollection<OptionResult<Guid>>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<Guid>>> Handle(
        GetDepartmentOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql =
            @"
SELECT
    Id AS Value,
    Name,
    IsEnabled
FROM HR.Department
ORDER BY Name
";
        var options = await _connection.QueryAsync<OptionResult<Guid>>(sql);
        return options.ToList();
    }
}
