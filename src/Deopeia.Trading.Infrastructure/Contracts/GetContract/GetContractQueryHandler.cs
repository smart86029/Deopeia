using Deopeia.Trading.Application.Contracts;
using Deopeia.Trading.Application.Contracts.GetContract;

namespace Deopeia.Trading.Infrastructure.Contracts.GetContract;

internal class GetContractQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetContractQuery, GetContractViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetContractViewModel> Handle(
        GetContractQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    symbol,
    currency_code,
    underlying_type,
    tick_size,
    contract_size_quantity,
    contract_size_unit_code,
    leverages
FROM contract
WHERE symbol = @Symbol;

SELECT
    culture,
    name,
    description
FROM contract_locale
WHERE symbol = @Symbol;
""";
        using var multiple = await _connection.QueryMultipleAsync(sql, new { request.Symbol });
        var result = multiple.ReadFirst<GetContractViewModel>();
        result.Locales = multiple.Read<ContractLocaleDto>().ToList();

        return result;
    }
}
