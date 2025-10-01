using Deopeia.Product.Domain.Instruments;
using Deopeia.Product.Domain.Instruments.Spots;

namespace Deopeia.Product.Application.Instruments.CreateInstrument;

public sealed class CreateInstrumentCommandHandler(
    IUnitOfWork unitOfWork,
    ISpotRepository spotRepository
) : ICommandHandler<CreateInstrumentCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISpotRepository _spotRepository = spotRepository;

    public async ValueTask<Unit> Handle(
        CreateInstrumentCommand command,
        CancellationToken cancellationToken
    )
    {
        var en = command.Localizations.First(x => x.Culture == "en");
        var priceConstraints = new PriceConstraints(command.PriceConstraints.TickSize);
        var quantityConstraints = new QuantityConstraints(
            command.QuantityConstraints.MinQuantity,
            command.QuantityConstraints.StepSize,
            command.QuantityConstraints.MinNotional
        );
        var spot = new Spot(
            new Symbol(command.Symbol),
            en.Name,
            command.BaseAsset,
            command.QuoteAsset,
            priceConstraints,
            quantityConstraints
        );
        foreach (var localization in command.Localizations)
        {
            var culture = CultureInfo.GetCultureInfo(localization.Culture);
            spot.Rename(localization.Name, culture);
        }

        _spotRepository.Add(spot);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
