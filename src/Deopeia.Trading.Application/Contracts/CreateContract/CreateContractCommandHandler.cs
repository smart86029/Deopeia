using Deopeia.Trading.Domain.Contracts;

namespace Deopeia.Trading.Application.Contracts.CreateContract;

public class CreateContractCommandHandler(
    ITradingUnitOfWork unitOfWork,
    IContractRepository contractRepository
) : IRequestHandler<CreateContractCommand>
{
    private readonly ITradingUnitOfWork _unitOfWork = unitOfWork;
    private readonly IContractRepository _contractRepository = contractRepository;

    public async Task Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var en = request.Locales.First(x => x.Culture == "en");

        var contract = new Contract(
            request.Symbol,
            en.Name,
            en.Description,
            request.UnderlyingType,
            new CurrencyCode(request.CurrencyCode),
            new ContractSize(
                request.ContractSizeQuantity,
                new Common.Domain.Measurement.UnitCode(request.ContractSizeUnitCode)
            ),
            request.TickSize
        );

        await _contractRepository.AddAsync(contract);
        await _unitOfWork.CommitAsync();
    }
}
