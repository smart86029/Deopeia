using System.Text.Json.Serialization;

namespace Viriplaca.HR.App.Departments.GetDepartments;

public class DepartmentDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public Guid? ParentId { get; set; }

    [JsonIgnore]
    public string FirstName { get; set; } = string.Empty;

    [JsonIgnore]
    public string? LastName { get; set; } = string.Empty;

    public string HeadName => $"{FirstName} {LastName}";

    public int EmployeeCount { get; set; }
}
