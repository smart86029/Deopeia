using Deopeia.Quote.Application.ContractSpecifications;
using Deopeia.Quote.Application.ContractSpecifications.GetContractSpecification;

namespace Deopeia.Quote.Infrastructure.Instruments.ContractSpecifications.GetContractSpecification;

internal class GetContractSpecificationQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetContractSpecificationQuery, GetContractSpecificationViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetContractSpecificationViewModel> Handle(
        GetContractSpecificationQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id,
    exchange_id,
    symbol,
    symbol_template,
    currency_code,
    underlying_asset_id,
    tick_size,
    contract_size_quantity,
    contract_size_unit_code
FROM contract_specification
WHERE id = @Id AND type = @Futures;

SELECT
    culture,
    name,
    name_template
FROM contract_specification_locale
WHERE contract_specification_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(
            sql,
            new { request.Id, InstrumentType.Futures }
        );
        var result = multiple.ReadFirst<GetContractSpecificationViewModel>();
        result.Locales = multiple.Read<ContractSpecificationLocaleDto>().ToList();

        return result;
    }
}
