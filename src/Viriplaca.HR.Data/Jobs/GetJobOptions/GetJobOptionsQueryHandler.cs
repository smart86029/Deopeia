using Viriplaca.HR.App.Jobs.GetJobOptions;

namespace Viriplaca.HR.Data.Jobs.GetJobOptions;

public class GetJobOptionsQueryHandler(SqlConnection connection)
    : IRequestHandler<GetJobOptionsQuery, ICollection<OptionResult<Guid>>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<Guid>>> Handle(
        GetJobOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql =
            @"
SELECT
    Id AS Value,
    Title AS Name,
    IsEnabled
FROM HR.Job
ORDER BY Title
";
        var options = await _connection.QueryAsync<OptionResult<Guid>>(sql);
        return options.ToList();
    }
}
