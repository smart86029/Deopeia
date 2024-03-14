namespace Viriplaca.HR.Domain.Jobs;

public class Job : AggregateRoot
{
    private Job()
    {
    }

    public Job(string title, bool isEnabled)
    {
        title.MustNotBeNullOrWhiteSpace();

        Title = title.Trim();
        IsEnabled = isEnabled;
    }

    public string Title { get; private set; } = string.Empty;

    public bool IsEnabled { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public void UpdateTitle(string title)
    {
        title.MustNotBeNullOrWhiteSpace();
        Title = title.Trim();
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }
}
