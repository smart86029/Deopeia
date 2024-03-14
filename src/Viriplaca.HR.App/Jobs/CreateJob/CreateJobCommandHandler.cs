using Viriplaca.HR.Domain.Jobs;

namespace Viriplaca.HR.App.Jobs.CreateJob;

public class CreateJobCommandHandler(IHRUnitOfWork unitOfWork, IJobRepository jobRepository)
    : IRequestHandler<CreateJobCommand, Guid>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJobRepository _jobRepository = jobRepository;

    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = new Job(request.Title, request.IsEnabled);

        _jobRepository.Add(job);
        await _unitOfWork.CommitAsync();

        return job.Id;
    }
}
