using Deopeia.Quote.Application.Options.GetCurrencies;

namespace Deopeia.Quote.Infrastructure.Options.GetCurrencies;

internal class GetCurrenciesQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetCurrenciesQuery, ICollection<OptionResult<string>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<string>>> Handle(
        GetCurrenciesQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.code AS value,
    CONCAT(COALESCE(b.name, c.name), ' (', a.code, ')') AS name
FROM currency AS a
LEFT JOIN currency_locale AS b ON a.code = b.currency_code AND b.culture = @CurrentCulture
INNER JOIN currency_locale AS c ON a.code = c.currency_code AND c.culture = @DefaultThreadCurrentCulture
""";
        var optoins = await _connection.QueryAsync<OptionResult<string>>(
            sql,
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );

        return optoins.ToList();
    }
}
