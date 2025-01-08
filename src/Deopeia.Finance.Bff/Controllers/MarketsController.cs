using Deopeia.Finance.Bff.Models.Contracts;

namespace Deopeia.Finance.Bff.Controllers;

public class MarketsController(ITradingApi tradingApi) : ApiController
{
    private readonly ITradingApi _tradingApi = tradingApi;

    [HttpGet("Stock")]
    public async Task<IActionResult> GetStock([FromQuery] GetContractsQuery query)
    {
        var result = await _tradingApi.GetContractsAsync(
            query with
            {
                UnderlyingType = UnderlyingType.Stock,
            }
        );

        return Ok(result);
    }

    [HttpGet("Index")]
    public async Task<IActionResult> GetIndex([FromQuery] GetContractsQuery query)
    {
        var result = await _tradingApi.GetContractsAsync(
            query with
            {
                UnderlyingType = UnderlyingType.Index,
            }
        );

        return Ok(result);
    }

    [HttpGet("Commodity")]
    public async Task<IActionResult> GetCommodity([FromQuery] GetContractsQuery query)
    {
        var result = await _tradingApi.GetContractsAsync(
            query with
            {
                UnderlyingType = UnderlyingType.Commodity,
            }
        );

        return Ok(result);
    }

    [HttpGet("Forex")]
    public async Task<IActionResult> GetForex([FromQuery] GetContractsQuery query)
    {
        var result = await _tradingApi.GetContractsAsync(
            query with
            {
                UnderlyingType = UnderlyingType.Forex,
            }
        );

        return Ok(result);
    }

    [HttpGet("Cryptocurrency")]
    public async Task<IActionResult> GetCryptocurrency([FromQuery] GetContractsQuery query)
    {
        var result = await _tradingApi.GetContractsAsync(
            query with
            {
                UnderlyingType = UnderlyingType.Cryptocurrency,
            }
        );

        return Ok(result);
    }
}
