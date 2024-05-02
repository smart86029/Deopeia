using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.App.Leaves.GetLeave;

public class LeaveDto
{
    public Guid Id { get; set; }

    public LeaveType Type { get; set; }

    public DateTimeOffset StartedAt { get; set; }

    public DateTimeOffset EndedAt { get; set; }

    public string Reason { get; set; } = string.Empty;

    public ApprovalStatus ApprovalStatus { get; set; }

    public EmployeeDto Employee { get; set; } = new();
}
