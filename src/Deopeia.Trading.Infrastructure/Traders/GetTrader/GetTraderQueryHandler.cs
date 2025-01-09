using Deopeia.Trading.Application.Traders.GetTrader;

namespace Deopeia.Trading.Infrastructure.Traders.GetTrader;

public class GetTraderQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetTraderQuery, GetTraderViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetTraderViewModel> Handle(
        GetTraderQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id,
    name,
    is_enabled
FROM trader
WHERE id = @Id
""";
        var result = await _connection.QuerySingleAsync<GetTraderViewModel>(sql, request);

        return result;
    }
}
