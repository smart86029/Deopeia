namespace Viriplaca.HR.App.Jobs.UpdateJob;

public record UpdateJobCommand(Guid Id, string Title, bool IsEnabled) : IRequest { }
