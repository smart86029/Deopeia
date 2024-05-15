using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.App.Leaves.CancelLeave;

internal class CancelLeaveCommandHandler(
    CurrentUser currentUser,
    IHRUnitOfWork unitOfWork,
    ILeaveRepository leaveRepository
) : IRequestHandler<CancelLeaveCommand>
{
    private readonly CurrentUser _currentUser = currentUser;
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILeaveRepository _leaveRepository = leaveRepository;

    public async Task Handle(CancelLeaveCommand request, CancellationToken cancellationToken)
    {
        var leave = await _leaveRepository.GetLeaveAsync(request.Id);
        if (leave.EmployeeId != _currentUser.Id)
        {
            throw new AccessDeniedException();
        }

        _leaveRepository.Remove(leave);
        await _unitOfWork.CommitAsync();
    }
}
