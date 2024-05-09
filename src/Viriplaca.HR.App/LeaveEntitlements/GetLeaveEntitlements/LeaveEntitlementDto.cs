namespace Viriplaca.HR.App.LeaveEntitlements.GetLeaveEntitlements;

public class LeaveEntitlementDto
{
    public Guid Id { get; set; }

    public DateOnly StartedOn { get; set; }

    public DateOnly EndedOn { get; set; }

    public WorkingTime GrantedTime { get; set; } = new();

    public WorkingTime UsedTime { get; set; } = new();

    public LeaveTypeDto LeaveType { get; set; } = new();
}
