using Deopeia.Finance.Bff.Models.Trading;

namespace Deopeia.ClientPortal.Bff.Controllers;

public class FavoritesController(ITradingApi tradingApi) : ApiController
{
    private readonly ITradingApi _tradingApi = tradingApi;

    [HttpPut("{symbol}")]
    public async Task<IActionResult> Like([FromRoute] string symbol)
    {
        await _tradingApi.LikeAsync(User.GetUserId(), symbol);

        return NoContent();
    }

    [HttpDelete("{symbol}")]
    public async Task<IActionResult> Dislike([FromRoute] string symbol)
    {
        await _tradingApi.DislikeAsync(User.GetUserId(), symbol);

        return NoContent();
    }
}
