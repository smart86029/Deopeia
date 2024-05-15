using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.App.Leaves.ApplyLeave;

public class ApplyLeaveCommandHandler(
    CurrentUser currentUser,
    IHRUnitOfWork unitOfWork,
    ILeaveRepository leaveRepository
) : IRequestHandler<ApplyLeaveCommand, Guid>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILeaveRepository _leaveRepository = leaveRepository;

    public async Task<Guid> Handle(ApplyLeaveCommand request, CancellationToken cancellationToken)
    {
        var leave = new Leave(
            request.LeaveTypeId,
            request.StartedAt,
            request.EndedAt,
            request.Reason,
            currentUser.Id
        );
        _leaveRepository.Add(leave);
        await _unitOfWork.CommitAsync();

        return leave.Id;
    }
}
