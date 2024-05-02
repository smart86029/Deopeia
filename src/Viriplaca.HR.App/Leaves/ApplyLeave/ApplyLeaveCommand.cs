using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.App.Leaves.ApplyLeave;

public record ApplyLeaveCommand(
    LeaveType Type,
    DateTimeOffset StartedAt,
    DateTimeOffset EndedAt,
    string Reason)
    : IRequest<Guid>
{
}
