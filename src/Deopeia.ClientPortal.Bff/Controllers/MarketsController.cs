using Deopeia.Finance.Bff.Models.Contracts;
using Deopeia.Finance.Bff.Models.Markets;
using Deopeia.Finance.Bff.Models.Trading;

namespace Deopeia.Finance.Bff.Controllers;

public class MarketsController(ITradingApi tradingApi) : ApiController
{
    private readonly ITradingApi _tradingApi = tradingApi;

    [HttpGet("Favorite")]
    public async Task<IActionResult> GetFavorite([FromQuery] GetContractsQuery query)
    {
        var contracts = await _tradingApi.GetContractsAsync(
            query with
            {
                TraderId = User.GetUserId(),
            }
        );
        var result = contracts.MapItem(x => new Contract
        {
            Symbol = x.Symbol,
            Name = x.Name,
            IsFavorite = true,
        });

        return Ok(result);
    }

    [HttpGet("Stock")]
    public async Task<IActionResult> GetStock([FromQuery] GetContractsQuery query)
    {
        var symbols = await _tradingApi.GetFavoritesAsync(User.GetUserId());
        var contracts = await _tradingApi.GetContractsAsync(
            query with
            {
                UnderlyingType = UnderlyingType.Stock,
            }
        );
        var result = contracts.MapItem(x => new Contract
        {
            Symbol = x.Symbol,
            Name = x.Name,
            IsFavorite = symbols.Contains(x.Symbol),
        });

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
