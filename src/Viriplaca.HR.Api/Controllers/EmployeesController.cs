using Viriplaca.HR.App.Employees.CreateEmployee;
using Viriplaca.HR.App.Employees.GetEmployee;
using Viriplaca.HR.App.Employees.GetEmployees;
using Viriplaca.HR.App.Employees.UpdateEmployee;

namespace Viriplaca.HR.Api.Controllers;

public class EmployeesController : ApiController<EmployeesController>
{
    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromQuery] GetEmployeesQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
    {
        var result = await Sender.Send(new GetEmployeeQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        var result = await Sender.Send(command);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] UpdateEmployeeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Sender.Send(command);

        return Ok();
    }
}
