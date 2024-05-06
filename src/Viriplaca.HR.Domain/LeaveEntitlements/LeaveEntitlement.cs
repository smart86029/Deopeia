using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.Domain.LeaveEntitlements;

public class LeaveEntitlement : AggregateRoot
{
    private LeaveEntitlement()
    {
    }

    public LeaveEntitlement(
        Guid employeeId,
        DateOnly startedOn,
        DateOnly endedOn,
        LeaveType type,
        decimal grantedDays)
    {
        EmployeeId = employeeId;
        StartedOn = startedOn;
        EndedOn = endedOn;
        Type = type;
        GrantedDays = grantedDays;
    }

    public Guid EmployeeId { get; private init; }

    public DateOnly StartedOn { get; private init; }

    public DateOnly EndedOn { get; private init; }

    public LeaveType Type { get; private init; }

    public decimal GrantedDays { get; private set; }

    public decimal UsedHours { get; private set; }
}
