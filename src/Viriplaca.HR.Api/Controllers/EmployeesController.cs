using Microsoft.AspNetCore.Mvc;
using Viriplaca.HR.App.Employees.GetEmployees;

namespace Viriplaca.HR.Api.Controllers;

public class EmployeesController : ApiController<EmployeesController>
{
    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromQuery] GetEmployeesQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
