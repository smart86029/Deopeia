using Deopeia.Common.Application.Pagination;

namespace Deopeia.Common.Infrastructure.Dapper;

public static class DbConnectionExtensions
{
    public static async Task<PagedResult<TItem>> QueryPagedResultAsync<TItem>(
        this IDbConnection connection,
        SqlBuilder builder,
        string counterSql,
        string selectorSql,
        PagedQuery<TItem> query
    )
    {
        var counter = builder.AddTemplate(counterSql);
        var count = await connection.ExecuteScalarAsync<int>(counter.RawSql, counter.Parameters);
        var result = query.ToResult(count);

        selectorSql = selectorSql.Replace("/**pagination**/", " LIMIT @Limit OFFSET @Offset ");
        builder.AddParameters(
            new { Limit = result.PageSize, Offset = (result.PageIndex - 1) * result.PageSize }
        );

        var selector = builder.AddTemplate(selectorSql);
        var items = await connection.QueryAsync<TItem>(selector.RawSql, selector.Parameters);
        result.Items = items.ToList();

        return result;
    }
}
