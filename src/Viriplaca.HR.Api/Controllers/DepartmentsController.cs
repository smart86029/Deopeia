using Viriplaca.HR.App.Departments.CreateDepartment;
using Viriplaca.HR.App.Departments.GetDepartment;
using Viriplaca.HR.App.Departments.GetDepartmentOptions;
using Viriplaca.HR.App.Departments.GetDepartments;
using Viriplaca.HR.App.Departments.UpdateDepartment;

namespace Viriplaca.HR.Api.Controllers;

public class DepartmentsController : ApiController<DepartmentsController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetDepartmentOptions(
        [FromQuery] GetDepartmentOptionsQuery query
    )
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments([FromQuery] GetDepartmentsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartment([FromRoute] Guid id)
    {
        var result = await Sender.Send(new GetDepartmentQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
    {
        var result = await Sender.Send(command);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(
        [FromRoute] Guid id,
        [FromBody] UpdateDepartmentCommand command
    )
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Sender.Send(command);

        return Ok();
    }
}
