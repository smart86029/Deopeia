using Deopeia.Trading.Domain.Contracts;

namespace Deopeia.Trading.Application.Contracts.UpdateContract;

public class UpdateContractCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IContractRepository contractRepository
) : IRequestHandler<UpdateContractCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IContractRepository _contractRepository = contractRepository;

    public async Task Handle(UpdateContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetContractAsync(new Symbol(request.Symbol));

        var removed = contract
            .Locales.Where(x => !request.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        contract.RemoveLocales(removed);

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            contract.UpdateName(locale.Name, culture);
        }

        await _unitOfWork.CommitAsync();
    }
}
