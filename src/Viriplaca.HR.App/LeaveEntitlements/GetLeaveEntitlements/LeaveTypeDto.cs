namespace Viriplaca.HR.App.LeaveEntitlements.GetLeaveEntitlements;

public class LeaveTypeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
