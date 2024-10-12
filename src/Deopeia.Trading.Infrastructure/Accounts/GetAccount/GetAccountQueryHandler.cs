using Deopeia.Trading.Application.Accounts.GetAccount;

namespace Deopeia.Trading.Infrastructure.Accounts.GetAccount;

public class GetAccountQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetAccountQuery, GetAccountViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetAccountViewModel> Handle(
        GetAccountQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id,
    account_number,
    is_enabled
FROM account
WHERE id = @Id
""";
        var result = await _connection.QuerySingleAsync<GetAccountViewModel>(sql, request);

        return result;
    }
}
