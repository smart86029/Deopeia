using Viriplaca.HR.App.Jobs.UpdateJob;
using Viriplaca.HR.Domain.Jobs;

namespace Viriplaca.HR.App.Jobs.CreateJob;

public class UpdateJobCommandHandler(IHRUnitOfWork unitOfWork, IJobRepository jobRepository)
    : IRequestHandler<UpdateJobCommand>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJobRepository _jobRepository = jobRepository;

    public async Task Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetJobAsync(request.Id);
        job.UpdateTitle(request.Title);
        if (request.IsEnabled)
        {
            job.Enable();
        }
        else
        {
            job.Disable();
        }

        _jobRepository.Update(job);
        await _unitOfWork.CommitAsync();
    }
}
