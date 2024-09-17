using System;
using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Domain.Strategies;

public class Strategy : AggregateRoot<StrategyId>, ILocalizable<StrategyLocale, StrategyId>
{
    private readonly EntityLocaleCollection<StrategyLocale, StrategyId> _locales = [];
    private readonly List<StrategyLeg> _legs = [];

    private Strategy() { }

    public Strategy(
        string name,
        string? description,
        bool isEnabled,
        string openExpression,
        string closeExpression
    )
    {
        _locales.Default.UpdateName(name);
        _locales.Default.UpdateDescription(description);
        IsEnabled = isEnabled;
        OpenExpression = openExpression;
        CloseExpression = closeExpression;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public bool IsEnabled { get; private set; }

    public string OpenExpression { get; private set; } = string.Empty;

    public string CloseExpression { get; private set; } = string.Empty;

    public IReadOnlyCollection<StrategyLocale> Locales => _locales.AsReadOnly();

    public IReadOnlyCollection<StrategyLeg> Legs => _legs.AsReadOnly();

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _locales[culture].UpdateDescription(description);
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void RemoveLocales(IEnumerable<StrategyLocale> locales)
    {
        _locales.Remove(locales);
    }

    public void AddLeg(OrderSide side, decimal ticks, decimal volume)
    {
        _legs.Add(new StrategyLeg(Id, _legs.Count + 1, side, ticks, volume));
    }
}
