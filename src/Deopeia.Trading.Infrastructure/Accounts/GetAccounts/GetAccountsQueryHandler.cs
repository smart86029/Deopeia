using Deopeia.Trading.Application.Accounts.GetAccounts;

namespace Deopeia.Trading.Infrastructure.Accounts.GetAccounts;

public class GetAccountsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetAccountsQuery, PageResult<AccountDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<AccountDto>> Handle(
        GetAccountsQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        if (request.IsEnabled.HasValue)
        {
            builder.Where("is_enabled = @IsEnabled", new { request.IsEnabled });
        }

        if (!request.CurrencyCode.IsNullOrWhiteSpace())
        {
            builder.Where("balance_currency_code = @CurrencyCode", new { request.CurrencyCode });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM account /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<AccountDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    id,
    account_number,
    is_enabled,
    balance_currency_code AS currency_code,
    balance_amount AS balance
FROM account
/**where**/
ORDER BY account_number
LIMIT @Limit
OFFSET @Offset
""",
            new
            {
                CultureInfo.CurrentCulture,
                CultureInfo.DefaultThreadCurrentCulture,
                result.Limit,
                result.Offset,
            }
        );
        var accounts = await _connection.QueryAsync<AccountDto>(sql.RawSql, sql.Parameters);
        result.Items = accounts.ToList();

        return result;
    }
}
