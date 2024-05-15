namespace Viriplaca.HR.App.Leaves.ApplyLeave;

public record ApplyLeaveCommand(
    Guid LeaveTypeId,
    DateTimeOffset StartedAt,
    DateTimeOffset EndedAt,
    string Reason
) : IRequest<Guid> { }
