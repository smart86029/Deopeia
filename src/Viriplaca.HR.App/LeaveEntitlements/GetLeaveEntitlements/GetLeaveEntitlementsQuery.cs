namespace Viriplaca.HR.App.LeaveEntitlements.GetLeaveEntitlements;

public record GetLeaveEntitlementsQuery(
    Guid EmployeeId,
    DateOnly Date)
    : IRequest<ICollection<LeaveEntitlementDto>>
{
}
