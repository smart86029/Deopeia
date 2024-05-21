namespace Viriplaca.HR.Domain.LeaveTypes;

public interface ILeaveTypeRepository : IRepository<LeaveType>
{
    Task<LeaveType> GetLeaveTypeAsync(Guid leaveTypeId);

    void Add(LeaveType leaveType);

    void Update(LeaveType leaveType);

    void Remove(LeaveType leaveType);
}
