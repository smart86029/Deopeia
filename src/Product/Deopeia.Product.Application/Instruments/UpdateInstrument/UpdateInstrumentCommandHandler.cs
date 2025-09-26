using Deopeia.Product.Domain.Instruments;
using Deopeia.Product.Domain.Instruments.Spots;

namespace Deopeia.Product.Application.Instruments.UpdateInstrument;

internal sealed class UpdateInstrumentCommandHandler(
    IUnitOfWork unitOfWork,
    ISpotRepository spotRepository
) : ICommandHandler<UpdateInstrumentCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISpotRepository _spotRepository = spotRepository;

    public async ValueTask<Unit> Handle(
        UpdateInstrumentCommand command,
        CancellationToken cancellationToken
    )
    {
        var spot =
            await _spotRepository.GetSpotAsync(new InstrumentId(command.Id))
            ?? throw new InvalidOperationException("Spot not found");

        foreach (var localization in command.Localizations)
        {
            var culture = CultureInfo.GetCultureInfo(localization.Culture);
            spot.Rename(localization.Name, culture);
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
