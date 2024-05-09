namespace Viriplaca.HR.Domain.LeaveEntitlements;

public class LeaveEntitlement : AggregateRoot
{
    private LeaveEntitlement()
    {
    }

    public LeaveEntitlement(
        Guid employeeId,
        Guid leaveTypeId,
        DateOnly startedOn,
        DateOnly endedOn,
        WorkingTime grantedTime)
    {
        EmployeeId = employeeId;
        LeaveTypeId = leaveTypeId;
        StartedOn = startedOn;
        EndedOn = endedOn;
        GrantedTime = grantedTime;
    }

    public Guid EmployeeId { get; private init; }

    public Guid LeaveTypeId { get; private init; }

    public DateOnly StartedOn { get; private init; }

    public DateOnly EndedOn { get; private init; }

    public WorkingTime GrantedTime { get; private set; } = new();

    public WorkingTime UsedTime { get; private set; } = new();
}
