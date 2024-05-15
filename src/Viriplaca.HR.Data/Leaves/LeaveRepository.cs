using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.Data.Leaves;

public class LeaveRepository(HRContext context) : ILeaveRepository
{
    private readonly DbSet<Leave> _leaves = context.Set<Leave>();

    public async Task<Leave> GetLeaveAsync(Guid leaveId)
    {
        var result =
            await _leaves.SingleOrDefaultAsync(x => x.Id == leaveId) ?? throw new Exception();

        return result;
    }

    public void Add(Leave leave)
    {
        _leaves.Add(leave);
    }

    public void Update(Leave leave)
    {
        _leaves.Update(leave);
    }

    public void Remove(Leave leave)
    {
        _leaves.Remove(leave);
    }
}
