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

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM account /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<AccountDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    id,
    account_number,
    is_enabled
FROM account
/**where**/
LIMIT @Limit
OFFSET @Offset
""",
            new { result.Limit, result.Offset }
        );
        var accounts = await _connection.QueryAsync<AccountDto>(sql.RawSql, sql.Parameters);
        result.Items = accounts.ToList();

        return result;
    }
}
