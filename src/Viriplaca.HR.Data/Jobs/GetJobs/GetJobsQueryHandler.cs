using Viriplaca.HR.App.Jobs.GetJobs;

namespace Viriplaca.HR.Data.Jobs.GetJobs;

public class GetJobsQueryHandler(SqlConnection connection)
    : IRequestHandler<GetJobsQuery, PageResult<JobDto>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<PageResult<JobDto>> Handle(
        GetJobsQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        if (!request.Title.IsNullOrWhiteSpace())
        {
            builder.Where("Title LIKE @Title", new { Title = $"%{request.Title}%" });
        }

        if (request.IsEnabled.HasValue)
        {
            builder.Where("IsEnabled = @IsEnabled", new { request.IsEnabled });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM HR.Job AS A /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = request.ToResult(count);

        if (count == 0)
        {
            return result;
        }

        var sql = builder.AddTemplate(
            @"
SELECT
    A.Id,
    A.Title,
    A.IsEnabled,
    B.EmployeeCount
FROM HR.Job AS A
LEFT JOIN (
    SELECT JobId, COUNT(*) AS EmployeeCount
    FROM HR.JobChange
    WHERE EndedAt IS NULL OR EndedAt >= GETDATE()
    GROUP BY JobId
) AS B ON A.Id = B.JobId
/**where**/
ORDER BY A.Id
OFFSET @Offset ROWS 
FETCH NEXT @Limit ROWS ONLY
"
        );
        builder.AddParameters(new { result.Limit, result.Offset });
        var leaves = await _connection.QueryAsync<JobDto>(sql.RawSql, sql.Parameters);
        result.Items = leaves.ToList();

        return result;
    }
}
