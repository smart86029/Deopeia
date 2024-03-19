using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.App.Employees.GetEmployee;

public class EmployeeDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public DateOnly BirthDate { get; set; }

    public Sex Sex { get; set; }

    public MaritalStatus MaritalStatus { get; set; }

    public Guid DepartmentId { get; set; }

    public Guid JobId { get; set; }
}
