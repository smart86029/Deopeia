namespace Viriplaca.HR.App.Leaves.GetLeave;

public record GetLeaveQuery(Guid Id)
    : IRequest<LeaveDto>
{
}
