namespace Deopeia.Trading.Application.Strategies.GetStrategies;

public class StrategyDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsEnabled { get; set; }
}
