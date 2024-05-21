using Viriplaca.HR.Domain.LeaveTypes;

namespace Viriplaca.HR.App.LeaveTypes.UpdateLeaveType;

internal class UpdateLeaveTypeCommandHandler(
    IHRUnitOfWork unitOfWork,
    ILeaveTypeRepository leaveTypeRepository
) : IRequestHandler<UpdateLeaveTypeCommand>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;

    public async Task Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetLeaveTypeAsync(request.Id);
        leaveType.UpdateCanCarryForward(request.CanCarryForward);

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            leaveType.UpdateName(locale.Name, culture);
            leaveType.UpdateDescription(locale.Description, culture);
        }

        _leaveTypeRepository.Update(leaveType);
        await _unitOfWork.CommitAsync();
    }
}
