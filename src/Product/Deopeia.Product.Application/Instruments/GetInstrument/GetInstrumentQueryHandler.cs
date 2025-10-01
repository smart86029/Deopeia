using Deopeia.Product.Domain.Instruments;
using Deopeia.Product.Domain.Instruments.Spots;

namespace Deopeia.Product.Application.Instruments.GetInstrument;

internal sealed class GetInstrumentQueryHandler(ISpotRepository spotRepository)
    : IQueryHandler<GetInstrumentQuery, GetInstrumentResult>
{
    private readonly ISpotRepository _spotRepository = spotRepository;

    public async ValueTask<GetInstrumentResult> Handle(
        GetInstrumentQuery query,
        CancellationToken cancellationToken
    )
    {
        var spot = await _spotRepository.GetSpotAsync(new InstrumentId(query.Id));
        return new GetInstrumentResult
        {
            Id = spot.Id.Guid,
            Type = spot.Type,
            Symbol = spot.Symbol.Value,
            BaseAsset = spot.BaseAsset,
            QuoteAsset = spot.QuoteAsset,
            PriceConstraints = new PriceConstraintsDto(spot.PriceConstraints.TickSize),
            QuantityConstraints = new QuantityConstraintsDto(
                spot.QuantityConstraints.MinQuantity,
                spot.QuantityConstraints.StepSize,
                spot.QuantityConstraints.MinNotional
            ),
            Localizations = spot
                .Localizations.Select(x => new InstrumentLocalizationDto
                {
                    Culture = x.Culture.Name,
                    Name = x.Name,
                })
                .ToList(),
        };
    }
}
