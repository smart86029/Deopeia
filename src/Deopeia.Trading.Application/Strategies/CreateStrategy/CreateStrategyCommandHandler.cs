using System.Data;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Deopeia.Common.Extensions;
using Deopeia.Trading.Domain.Strategies;

namespace Deopeia.Trading.Application.Strategies.CreateStrategy;

public class CreateStrategyCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IStrategyRepository strategyRepository
) : IRequestHandler<CreateStrategyCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStrategyRepository _strategyRepository = strategyRepository;

    public async Task Handle(CreateStrategyCommand request, CancellationToken cancellationToken)
    {
        var en = request.Locales.First(x => x.Culture == "en");
        var strategy = new Strategy(
            en.Name,
            en.Description,
            request.IsEnabled,
            request.OpenExpression,
            request.CloseExpression
        );

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            strategy.UpdateName(locale.Name, culture);
            strategy.UpdateDescription(locale.Description, culture);
        }

        foreach (var leg in request.Legs)
        {
            strategy.AddLeg(leg.Side, leg.Ticks, leg.Volume);
        }

        _strategyRepository.Add(strategy);
        await _unitOfWork.CommitAsync();
    }
}
