using Microsoft.AspNetCore.Mvc;

namespace Deopeia.Finance.Bff.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IHubContext<RealTimeHub, IRealTime> hubContext)
    : ControllerBase
{
    private readonly IHubContext<RealTimeHub, IRealTime> _hubContext = hubContext;

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
