using Deopeia.Quote.Application.Exchanges;
using Deopeia.Quote.Application.Exchanges.GetExchange;

namespace Deopeia.Quote.Infrastructure.Exchanges.GetExchange;

public class GetExchangeQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetExchangeQuery, GetExchangeViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetExchangeViewModel> Handle(
        GetExchangeQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id AS mic,
    time_zone
FROM exchange
WHERE id = @Mic;

SELECT
    culture,
    name,
    abbreviation
FROM exchange_locale
WHERE exchange_id = @Mic;
""";
        using var multiple = await _connection.QueryMultipleAsync(sql, request);
        var result = multiple.ReadFirst<GetExchangeViewModel>();
        result.Locales = multiple.Read<ExchangeLocaleDto>().ToList();

        return result;
    }
}
