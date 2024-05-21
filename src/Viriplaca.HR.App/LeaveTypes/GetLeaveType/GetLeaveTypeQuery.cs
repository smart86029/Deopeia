namespace Viriplaca.HR.App.LeaveTypes.GetLeaveType;

public record GetLeaveTypeQuery(Guid Id) : IRequest<LeaveTypeDto> { }
