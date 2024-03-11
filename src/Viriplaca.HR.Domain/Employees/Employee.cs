using Viriplaca.HR.Domain.Departments;
using Viriplaca.HR.Domain.Jobs;
using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.Domain.Employees;

public class Employee : Person
{
    private readonly List<JobChange> _jobChanges = [];

    private Employee()
    {
    }

    public Employee(string firstName, string? lastName, DateOnly birthDate, Sex gender, MaritalStatus maritalStatus)
        : base(firstName, lastName, birthDate, gender, maritalStatus)
    {
        AddDomainEvent(new EmployeeCreated(Id));
    }

    private JobChange LastJobChange =>
        _jobChanges.SingleOrDefault(x => DateTimeOffset.UtcNow.IsBetween(x.StartedAt, x.EndedAt)) ?? _jobChanges.Last();

    public Guid DepartmentId { get; private set; }

    public Guid JobId { get; private set; }

    public bool IsEmployed => _jobChanges.Any(x => DateTimeOffset.UtcNow.IsBetween(x.StartedAt, x.EndedAt));

    public IReadOnlyCollection<JobChange> JobChanges => _jobChanges.AsReadOnly();

    public void AssignJob(Department department, Job job, bool isHead, DateTimeOffset startedAt)
    {
        _jobChanges.Add(new JobChange(Id, department.Id, job.Id, isHead, startedAt));
        DepartmentId = department.Id;
        JobId = job.Id;
    }
}
