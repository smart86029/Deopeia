namespace Viriplaca.HR.Domain.Leaves;

public interface ILeaveRepository : IRepository<Leave>
{
    Task<Leave> GetLeaveAsync(Guid leaveId);

    void Add(Leave leave);

    void Update(Leave leave);

    void Remove(Leave leave);
}
