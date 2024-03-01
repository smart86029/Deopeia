using Viriplaca.HR.Domain;

namespace Viriplaca.HR.App.Leaves.GetLeaves;

public record GetLeavesQuery : PageQuery<LeaveDto>
{
    public DateTimeOffset StartedAt { get; init; }

    public DateTimeOffset EndedAt { get; init; }

    public ApprovalStatus? ApprovalStatus { get; init; }
}
