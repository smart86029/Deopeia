namespace Viriplaca.HR.App.LeaveTypes.GetLeaveType;

public class LeaveTypeDto
{
    public Guid Id { get; set; }

    public bool CanCarryForward { get; set; }

    public List<LeaveTypeLocaleDto> Locales { get; set; } = [];
}
