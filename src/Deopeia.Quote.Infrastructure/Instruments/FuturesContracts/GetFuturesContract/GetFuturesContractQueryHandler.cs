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
    id,
    exchange_id,
    symbol,
    currency_code,
    underlying_asset_id,
    tick_size,
    contract_size_quantity,
    contract_size_unit_code
FROM instrument
WHERE id = @Id AND type = @Futures;

SELECT
    culture,
    name
FROM instrument_locale
WHERE instrument_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(
            sql,
            new { request.Id, InstrumentType.Futures }
        );
        var result = multiple.ReadFirst<GetFuturesContractViewModel>();
        result.Locales = multiple.Read<FuturesContractLocaleDto>().ToList();

        return result;
    }
}
