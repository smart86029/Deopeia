namespace Viriplaca.HR.App.Departments.GetDepartment;

public class DepartmentDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public Guid? ParentId { get; set; }
}
