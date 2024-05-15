using Viriplaca.HR.Domain.Jobs;

namespace Viriplaca.HR.Data.Jobs;

public class JobRepository(HRContext context) : IJobRepository
{
    private readonly DbSet<Job> _jobs = context.Set<Job>();

    public async Task<ICollection<Job>> GetJobsAsync()
    {
        var results = await _jobs.Where(x => x.IsEnabled).ToListAsync();

        return results;
    }

    public async Task<Job> GetJobAsync(Guid jobId)
    {
        var result =
            await _jobs.SingleOrDefaultAsync(x => x.Id == jobId)
            ?? throw new Exception(jobId.ToString());

        return result;
    }

    public void Add(Job job)
    {
        _jobs.Add(job);
    }

    public void Update(Job job)
    {
        _jobs.Update(job);
    }

    public void Remove(Job job)
    {
        _jobs.Remove(job);
    }
}
