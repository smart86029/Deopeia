using Deopeia.Trading.Application.Assets.CreateAsset;
using Deopeia.Trading.Application.Assets.GetAsset;
using Deopeia.Trading.Application.Assets.GetAssets;
using Deopeia.Trading.Application.Assets.UpdateAsset;

namespace Deopeia.Trading.Api.Controllers;

[AllowAnonymous]
public class AssetsController : ApiController<AssetsController>
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAssetsQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetAssetQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssetCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateAssetCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
