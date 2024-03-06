namespace Viriplaca.HR.App.Employees.GetEmployees;

public class EmployeeDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Name => $"{FirstName} {LastName}";

    public string DepartmentName { get; set; } = string.Empty;

    public string JobTitle { get; set; } = string.Empty;
}
