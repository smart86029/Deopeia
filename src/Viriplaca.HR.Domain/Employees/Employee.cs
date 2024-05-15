using Viriplaca.Common.Files;
using Viriplaca.HR.Domain.Departments;
using Viriplaca.HR.Domain.Jobs;
using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.Domain.Employees;

public class Employee : Person
{
    private readonly List<JobChange> _jobChanges = [];

    private Employee() { }

    public Employee(
        string firstName,
        string? lastName,
        DateOnly birthDate,
        Sex sex,
        MaritalStatus maritalStatus
    )
        : base(PersonType.Employee, firstName, lastName, birthDate, sex, maritalStatus)
    {
        AddDomainEvent(new EmployeeCreated(Id));
    }

    public Guid? AvatarId { get; private set; }

    public Guid DepartmentId { get; private set; }

    public Guid JobId { get; private set; }

    public bool IsEmployed =>
        _jobChanges.Any(x => DateTimeOffset.UtcNow.IsBetween(x.StartedAt, x.EndedAt));

    public IReadOnlyCollection<JobChange> JobChanges => _jobChanges.AsReadOnly();

    public void UpdateAvatar(Image image)
    {
        AvatarId = image.Id;
    }

    public void AssignJob(Department department, Job job, bool isHead, DateTimeOffset startedAt)
    {
        _jobChanges.Add(new JobChange(Id, department.Id, job.Id, isHead, startedAt));
        DepartmentId = department.Id;
        JobId = job.Id;
    }
}
