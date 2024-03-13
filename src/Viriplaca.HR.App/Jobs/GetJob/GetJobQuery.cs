namespace Viriplaca.HR.App.Jobs.GetJob;

public record GetJobQuery(Guid Id)
    : IRequest<JobDto>
{
}
