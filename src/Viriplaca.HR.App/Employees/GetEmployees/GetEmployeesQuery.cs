namespace Viriplaca.HR.App.Employees.GetEmployees;

public record GetEmployeesQuery(Guid? DepartmentId, Guid? JobId) : PageQuery<EmployeeDto> { }
