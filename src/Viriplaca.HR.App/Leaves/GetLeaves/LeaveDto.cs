namespace Viriplaca.HR.App.Leaves.GetLeaves;

public class LeaveDto
{
    public Guid Id { get; set; }

    public Guid LeaveTypeId { get; set; }

    public DateTimeOffset StartedAt { get; set; }

    public DateTimeOffset EndedAt { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; }

    public EmployeeDto Employee { get; set; } = new();
}
