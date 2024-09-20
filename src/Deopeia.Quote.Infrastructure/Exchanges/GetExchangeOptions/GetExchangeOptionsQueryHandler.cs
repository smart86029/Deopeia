using Deopeia.Quote.Application.Exchanges.GetExchangeOptions;

namespace Deopeia.Quote.Infrastructure.Exchanges.GetExchangeOptions;

internal class GetExchangeOptionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetExchangeOptionsQuery, ICollection<OptionResult<string>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<string>>> Handle(
        GetExchangeOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id AS value,
    COALESCE(b.name, c.name) AS name
FROM exchange AS a
LEFT JOIN exchange_locale AS b ON a.id = b.exchange_id AND b.culture = @CurrentCulture
INNER JOIN exchange_locale AS c ON a.id = c.exchange_id AND c.culture = @DefaultThreadCurrentCulture
""";
        var optoins = await _connection.QueryAsync<OptionResult<string>>(
            sql,
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );

        return optoins.ToList();
    }
}
