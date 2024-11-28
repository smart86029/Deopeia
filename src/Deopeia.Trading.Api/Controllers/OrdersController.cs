using Deopeia.Trading.Application.Orders.PlaceOrder;

namespace Deopeia.Trading.Api.Controllers;

[AllowAnonymous]
public class OrdersController : ApiController<OrdersController>
{
    [HttpPost]
    public async Task<IActionResult> Place([FromBody] PlaceOrderCommand command)
    {
        await Sender.Send(command);

        return NoContent();
    }
}
