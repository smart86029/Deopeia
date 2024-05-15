namespace Viriplaca.HR.Domain.Employees
{
    public class JobChange : Entity
    {
        private JobChange() { }

        public JobChange(
            Guid employeeId,
            Guid departmentId,
            Guid jobId,
            bool isHead,
            DateTimeOffset startedAt
        )
        {
            EmployeeId = employeeId;
            DepartmentId = departmentId;
            JobId = jobId;
            IsHead = isHead;
            StartedAt = startedAt;
        }

        public Guid EmployeeId { get; private set; }

        public Guid DepartmentId { get; private set; }

        public Guid JobId { get; private set; }

        public bool IsHead { get; private set; }

        public DateTimeOffset StartedAt { get; private set; }

        public DateTimeOffset? EndedAt { get; private set; }
    }
}
