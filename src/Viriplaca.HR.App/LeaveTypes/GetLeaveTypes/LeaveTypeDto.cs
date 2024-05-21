namespace Viriplaca.HR.App.LeaveTypes.GetLeaveTypes;

public class LeaveTypeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public bool CanCarryForward { get; set; }
}
