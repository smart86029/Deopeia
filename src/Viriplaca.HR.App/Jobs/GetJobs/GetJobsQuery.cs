namespace Viriplaca.HR.App.Jobs.GetJobs;

public record GetJobsQuery(string? Title, bool? IsEnabled) : PageQuery<JobDto> { }
