using Viriplaca.HR.App.Jobs.GetJob;

namespace Viriplaca.HR.Data.Jobs.GetJobs;

public class GetJobQueryHandler(SqlConnection connection)
    : IRequestHandler<GetJobQuery, JobDto>
{
    private readonly SqlConnection _connection = connection;

    public async Task<JobDto> Handle(GetJobQuery request, CancellationToken cancellationToken)
    {
        var sql = @"
SELECT
    Id,
    Title,
    IsEnabled
FROM HR.Job
WHERE Id = @Id
";
        var result = await _connection.QueryFirstAsync<JobDto>(sql, request);

        return result;
    }
}
