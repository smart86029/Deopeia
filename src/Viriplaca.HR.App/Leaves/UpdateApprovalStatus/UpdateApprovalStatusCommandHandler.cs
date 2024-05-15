using Viriplaca.HR.Domain.Employees;
using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.App.Leaves.UpdateApprovalStatus;

internal class UpdateApprovalStatusCommandHandler(
    CurrentUser currentUser,
    IHRUnitOfWork unitOfWork,
    ILeaveRepository leaveRepository,
    IEmployeeRepository employeeRepository
) : IRequestHandler<UpdateApprovalStatusCommand>
{
    private readonly CurrentUser _currentUser = currentUser;
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILeaveRepository _leaveRepository = leaveRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task Handle(
        UpdateApprovalStatusCommand request,
        CancellationToken cancellationToken
    )
    {
        var leave = await _leaveRepository.GetLeaveAsync(request.Id);
        var supervisor = await _employeeRepository.GetSupervisorAsync(leave.EmployeeId);
        if (supervisor?.Id != _currentUser.Id)
        {
            throw new AccessDeniedException();
        }

        switch (request.ApprovalStatus)
        {
            case ApprovalStatus.Approved:
                leave.Approve();
                break;

            case ApprovalStatus.Rejected:
                leave.Reject();
                break;
        }

        _leaveRepository.Update(leave);
        await _unitOfWork.CommitAsync();
    }
}
