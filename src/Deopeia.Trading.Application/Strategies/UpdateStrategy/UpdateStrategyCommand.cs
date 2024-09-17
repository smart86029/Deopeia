namespace Deopeia.Trading.Application.Strategies.UpdateStrategy;

public record UpdateStrategyCommand(
    Guid Id,
    bool IsEnabled,
    string OpenExpression,
    string CloseExpression,
    ICollection<StrategyLocaleDto> Locales,
    ICollection<StrategyLegDto> Legs
) : IRequest { }
