namespace Viriplaca.HR.App.Leaves.UpdateApprovalStatus;

public record UpdateApprovalStatusCommand(
    Guid Id,
    ApprovalStatus ApprovalStatus)
    : IRequest
{
}
