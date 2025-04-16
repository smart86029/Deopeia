using Deopeia.Finance.Bff.Models.Trading;

namespace Deopeia.Finance.Bff.Controllers;

public class AssetsController(ITradingApi tradingApi) : ApiController
{
    private readonly ITradingApi _tradingApi = tradingApi;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var accounts = await _tradingApi.GetAccountsAsync(User.GetUserId());

        return Ok(accounts);
    }
}
