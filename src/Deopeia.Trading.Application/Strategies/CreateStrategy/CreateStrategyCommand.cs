namespace Deopeia.Trading.Application.Strategies.CreateStrategy;

public record CreateStrategyCommand(
    bool IsEnabled,
    string OpenExpression,
    string CloseExpression,
    ICollection<StrategyLocaleDto> Locales,
    ICollection<StrategyLegDto> Legs
) : IRequest { }
