namespace Viriplaca.HR.App.Employees.GetEmployee;

public record GetEmployeeQuery(Guid Id)
    : IRequest<EmployeeDto>
{
}
