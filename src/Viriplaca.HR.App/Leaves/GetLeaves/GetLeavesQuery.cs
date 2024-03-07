namespace Viriplaca.HR.App.Leaves.GetLeaves;

public record GetLeavesQuery(
    DateTimeOffset StartedAt,
    DateTimeOffset EndedAt,
    ApprovalStatus? ApprovalStatus)
    : PageQuery<LeaveDto>
{
}
