namespace Viriplaca.HR.App.LeaveTypes.UpdateLeaveType;

public record UpdateLeaveTypeCommand : IRequest
{
    public Guid Id { get; set; }

    public bool CanCarryForward { get; set; }

    public List<LeaveTypeLocaleDto> Locales { get; set; } = [];
}
