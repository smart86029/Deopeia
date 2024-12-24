using Deopeia.Finance.Bff.Models.Instruments;
using ContractSize = Deopeia.Finance.Bff.Models.Instruments.ContractSize;

namespace Deopeia.Finance.Bff.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstrumentsController(ITradingApi tradingApi) : ControllerBase
{
    private readonly ITradingApi _tradingApi = tradingApi;

    [HttpGet("{symbol}")]
    public async Task<IActionResult> Get([FromRoute] string symbol)
    {
        var contract = await _tradingApi.GetContractAsync(symbol);
        var locale = contract.Locales.Single(x => x.Culture == CultureInfo.CurrentCulture.Name);
        var result = new Instrument
        {
            Symbol = contract.Symbol,
            Name = locale.Name,
            UnderlyingType = contract.UnderlyingType,
            CurrencyCode = contract.CurrencyCode,
            PricePrecision = contract.PricePrecision,
            TickSize = contract.TickSize,
            ContractSize = new ContractSize
            {
                Quantity = contract.ContractSizeQuantity,
                UnitCode = contract.ContractSizeUnitCode,
            },
            VolumeRestriction = new VolumeRestriction
            {
                Min = contract.VolumeRestrictionMin,
                Max = contract.VolumeRestrictionMax,
                Step = contract.VolumeRestrictionStep,
            },
            Sessions = contract.Sessions,
            Leverages = contract.Leverages,
        };

        return Ok(result);
    }
}
