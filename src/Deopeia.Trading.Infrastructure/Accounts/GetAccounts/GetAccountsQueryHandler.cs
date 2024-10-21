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
            builder.Where("a.is_enabled = @IsEnabled", new { request.IsEnabled });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM account AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<AccountDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.id,
    a.account_number,
    a.is_enabled,
    COALESCE(b.name, c.name) AS currency
FROM account AS a
LEFT JOIN currency_locale AS b
    ON a.balance_currency_code = b.currency_code AND b.culture = @CurrentCulture
INNER JOIN currency_locale AS c
    ON a.balance_currency_code = c.currency_code AND c.culture = @DefaultThreadCurrentCulture
/**where**/
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
