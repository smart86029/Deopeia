using Deopeia.Trading.Domain.Strategies;

namespace Deopeia.Trading.Application.Strategies.UpdateStrategy;

public class UpdateStrategyCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IStrategyRepository strategyRepository
) : IRequestHandler<UpdateStrategyCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStrategyRepository _strategyRepository = strategyRepository;

    public async Task Handle(UpdateStrategyCommand request, CancellationToken cancellationToken)
    {
        var strategy = await _strategyRepository.GetStrategyAsync(new StrategyId(request.Id));
        if (request.IsEnabled)
        {
            strategy.Enable();
        }
        else
        {
            strategy.Disable();
        }

        var removed = strategy
            .Locales.Where(x => !request.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        strategy.RemoveLocales(removed);

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            strategy.UpdateName(locale.Name, culture);
            strategy.UpdateDescription(locale.Description, culture);
        }

        await _unitOfWork.CommitAsync();
    }
}
