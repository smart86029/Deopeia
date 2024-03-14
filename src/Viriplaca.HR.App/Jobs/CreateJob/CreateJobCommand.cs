namespace Viriplaca.HR.App.Jobs.CreateJob;

public record CreateJobCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }
}
