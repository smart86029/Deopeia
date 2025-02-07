using Deopeia.Trading.Application.Traders.GetAccounts;

namespace Deopeia.Trading.Infrastructure.Traders.GetAccounts;

public class GetAccountsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetAccountsQuery, ICollection<AccountDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<AccountDto>> Handle(
        GetAccountsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.currency_code,
    a.balance,
    b.exchange_rate
FROM account AS a
INNER JOIN currency AS b ON a.currency_code = b.code
WHERE a.trader_id = @TraderId
""";
        var results = await _connection.QueryAsync<AccountDto>(sql, request);

        return results.ToList();
    }
}
