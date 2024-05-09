namespace Viriplaca.HR.Domain.LeaveTypes;

public class LeaveType : AggregateRoot, ILocalizable<LeaveTypeLocale>
{
    private readonly EntityLocaleCollection<LeaveTypeLocale> _locales = [];

    private LeaveType()
    {
    }

    public LeaveType(string name, string? description)
        : this(name, description, false)
    {
    }

    public LeaveType(string name, string? description, bool canCarryForward)
    {
        var enUS = CultureInfo.GetCultureInfo("en-US");
        UpdateName(name, enUS);
        UpdateDescription(description, enUS);
        CanCarryForward = canCarryForward;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public bool CanCarryForward { get; private set; }

    public IReadOnlyCollection<LeaveTypeLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _locales[culture].UpdateDescription(description);
    }
}
