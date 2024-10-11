using Deopeia.Quote.Domain.Instruments.FuturesContracts;

namespace Deopeia.Quote.Application.FuturesContracts.UpdateFuturesContract;

public class UpdateFuturesContractCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IFuturesContractRepository futuresContractRepository
) : IRequestHandler<UpdateFuturesContractCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFuturesContractRepository _futuresContractRepository =
        futuresContractRepository;

    public async Task Handle(
        UpdateFuturesContractCommand request,
        CancellationToken cancellationToken
    )
    {
        var futuresContract = await _futuresContractRepository.GetFuturesContractAsync(
            new InstrumentId(request.Id)
        );
        futuresContract.UpdateExpirationDate(request.ExpirationDate);

        var removed = futuresContract
            .Locales.Where(x => !request.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        futuresContract.RemoveLocales(removed);

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            futuresContract.UpdateName(locale.Name, culture);
        }

        await _unitOfWork.CommitAsync();
    }
}
