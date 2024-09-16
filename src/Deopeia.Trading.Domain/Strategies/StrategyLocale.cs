namespace Deopeia.Trading.Domain.Strategies;

public class StrategyLocale : EntityLocale<StrategyId>
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    internal void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name.Trim();
    }

    internal void UpdateDescription(string? description)
    {
        Description = description?.Trim();
    }
}
