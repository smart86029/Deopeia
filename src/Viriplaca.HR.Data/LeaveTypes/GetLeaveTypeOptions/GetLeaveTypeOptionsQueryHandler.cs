using Viriplaca.HR.App.LeaveTypes.GetLeaveTypeOptions;

namespace Viriplaca.HR.Data.LeaveTypes.GetLeaveTypeOptions;

public class GetLeaveTypeOptionsQueryHandler(SqlConnection connection)
    : IRequestHandler<GetLeaveTypeOptionsQuery, ICollection<OptionResult<Guid>>>
{
    private readonly SqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<Guid>>> Handle(
        GetLeaveTypeOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql =
            @"
SELECT
    A.Id AS Value,
    B.Name
FROM HR.LeaveType AS A
INNER JOIN HR.LeaveTypeLocale AS B ON A.Id = B.LeaveTypeId AND B.Culture = @Culture
ORDER BY A.Id
";
        var param = new { Culture = CultureInfo.CurrentCulture, };
        var options = await _connection.QueryAsync<OptionResult<Guid>>(sql, param);

        return options.ToList();
    }
}
