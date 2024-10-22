using Deopeia.Trading.Application.Positions.GetPosition;

namespace Deopeia.Trading.Infrastructure.Positions.GetPosition;

internal class GetPositionQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetPositionQuery, GetPositionViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetPositionViewModel> Handle(
        GetPositionQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id,
    b.account_number,
    a.type,
    a.volume
FROM position AS a
INNER JOIN account AS b ON a.opened_by = b.id
WHERE a.id = @Id
""";
        var result = await _connection.QuerySingleAsync<GetPositionViewModel>(sql, request);

        return result;
    }
}
