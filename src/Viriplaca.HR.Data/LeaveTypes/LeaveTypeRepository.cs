using Viriplaca.HR.Domain.LeaveTypes;

namespace Viriplaca.HR.Data.LeaveTypes;

internal class LeaveTypeRepository(HRContext context) : ILeaveTypeRepository
{
    private readonly DbSet<LeaveType> _leaveTypes = context.Set<LeaveType>();

    public async Task<LeaveType> GetLeaveTypeAsync(Guid leaveTypeId)
    {
        var result = await _leaveTypes
            .Include(x => x.Locales)
            .SingleOrDefaultAsync(x => x.Id == leaveTypeId);

        return result ?? throw new Exception();
    }

    public void Add(LeaveType leaveType)
    {
        _leaveTypes.Add(leaveType);
    }

    public void Update(LeaveType leaveType)
    {
        _leaveTypes.Update(leaveType);
    }

    public void Remove(LeaveType leaveType)
    {
        _leaveTypes.Remove(leaveType);
    }
}
