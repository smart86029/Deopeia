using Deopeia.Trading.Application.Traders.GetFavorites;

namespace Deopeia.Trading.Infrastructure.Traders.GetFavorites;

public class GetFavoritesQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetFavoritesQuery, ICollection<string>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<string>> Handle(
        GetFavoritesQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT symbol
FROM trader_favorite
WHERE trader_id = @TraderId
""";
        var results = await _connection.QueryAsync<string>(sql, request);

        return results.ToList();
    }
}
