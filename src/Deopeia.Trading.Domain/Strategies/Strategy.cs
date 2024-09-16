namespace Deopeia.Trading.Domain.Strategies;

public class Strategy : AggregateRoot<StrategyId>, ILocalizable<StrategyLocale, StrategyId>
{
    private readonly EntityLocaleCollection<StrategyLocale, StrategyId> _locales = [];
    private readonly List<StrategyLeg> _strategyLegs = [];

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public IReadOnlyCollection<StrategyLocale> Locales => _locales.AsReadOnly();

    public IReadOnlyCollection<StrategyLeg> StrategyLegs => _strategyLegs.AsReadOnly();

    public void RemoveLocales(IEnumerable<StrategyLocale> locales)
    {
        _locales.Remove(locales);
    }
}
