namespace Deopeia.Trading.Application.Strategies.GetStrategy;

public class GetStrategyViewModel
{
    public Guid Id { get; set; }

    public bool IsEnabled { get; set; }

    public string OpenExpression { get; set; } = string.Empty;

    public string CloseExpression { get; set; } = string.Empty;

    public ICollection<StrategyLocaleDto> Locales { get; set; } = [];
}
