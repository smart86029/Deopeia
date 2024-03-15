namespace Viriplaca.HR.App.Jobs.CreateJob;

public record CreateJobCommand(string Title, bool IsEnabled)
    : IRequest<Guid>
{
}
