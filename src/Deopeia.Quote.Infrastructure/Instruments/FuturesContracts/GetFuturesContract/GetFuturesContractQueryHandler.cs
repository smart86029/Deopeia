using Deopeia.Quote.Application.FuturesContracts;
using Deopeia.Quote.Application.FuturesContracts.GetFuturesContract;

namespace Deopeia.Quote.Infrastructure.Instruments.FuturesContracts.GetFuturesContract;

internal class GetFuturesContractQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetFuturesContractQuery, GetFuturesContractViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetFuturesContractViewModel> Handle(
        GetFuturesContractQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id,
    a.symbol,
    a.expiration_date,
    c.name AS exchange,
    d.name AS underlying_asset,
    CONCAT(e.name, ' (', a.currency_code, ')') AS currency,
    b.tick_size,
    b.contract_size_quantity,
    CONCAT(g.name, CASE WHEN f.symbol IS NOT NULL THEN CONCAT(' (', f.symbol, ')') END) AS contract_size_unit
FROM instrument AS a
INNER JOIN contract_specification AS b ON a.contract_specification_id = b.id
INNER JOIN exchange_locale AS c ON a.exchange_id = c.exchange_id AND c.culture = @CurrentCulture
INNER JOIN asset_locale AS d ON b.underlying_asset_id = d.asset_id AND d.culture = @CurrentCulture
INNER JOIN currency_locale AS e ON a.currency_code = e.currency_code AND e.culture = @CurrentCulture
INNER JOIN unit AS f ON b.contract_size_unit_code = f.code
INNER JOIN unit_locale AS g ON b.contract_size_unit_code = g.unit_code AND g.culture = @CurrentCulture
WHERE a.id = @Id AND a.type = @Futures;

SELECT
    culture,
    name
FROM instrument_locale
WHERE instrument_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(
            sql,
            new
            {
                request.Id,
                InstrumentType.Futures,
                CultureInfo.CurrentCulture,
            }
        );
        var result = multiple.ReadFirst<GetFuturesContractViewModel>();
        result.Locales = multiple.Read<FuturesContractLocaleDto>().ToList();

        return result;
    }
}
