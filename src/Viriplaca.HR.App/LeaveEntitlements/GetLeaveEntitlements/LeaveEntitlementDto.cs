using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.App.LeaveEntitlements.GetLeaveEntitlements;

public class LeaveEntitlementDto
{
    public Guid Id { get; set; }

    public DateOnly StartedOn { get; set; }

    public DateOnly EndedOn { get; set; }

    public LeaveType Type { get; set; }

    public decimal AvailableHours { get; set; }

    public decimal UsedHours { get; set; }
}
