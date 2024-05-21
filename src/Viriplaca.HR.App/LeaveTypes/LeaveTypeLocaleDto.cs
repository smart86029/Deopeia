namespace Viriplaca.HR.App.LeaveTypes;

public class LeaveTypeLocaleDto
{
    public string Culture { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
