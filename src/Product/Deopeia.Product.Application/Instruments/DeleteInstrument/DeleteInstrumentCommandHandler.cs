using Deopeia.Product.Domain.Instruments;
using Deopeia.Product.Domain.Instruments.Spots;

namespace Deopeia.Product.Application.Instruments.DeleteInstrument;

internal sealed class DeleteInstrumentCommandHandler(
    IUnitOfWork unitOfWork,
    ISpotRepository spotRepository
) : ICommandHandler<DeleteInstrumentCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISpotRepository _spotRepository = spotRepository;

    public async ValueTask<Unit> Handle(
        DeleteInstrumentCommand command,
        CancellationToken cancellationToken
    )
    {
        var spot = await _spotRepository.GetSpotAsync(new InstrumentId(command.Id));
        _spotRepository.Remove(spot);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
