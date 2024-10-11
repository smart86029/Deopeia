using Deopeia.Quote.Domain.ContractSpecifications;
using Deopeia.Quote.Domain.Instruments.FuturesContracts;

namespace Deopeia.Quote.Application.FuturesContracts.CreateFuturesContract;

public class CreateFuturesContractCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IContractSpecificationRepository contractSpecificationRepository,
    IFuturesContractRepository futuresContractRepository
) : IRequestHandler<CreateFuturesContractCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IContractSpecificationRepository _contractSpecificationRepository =
        contractSpecificationRepository;
    private readonly IFuturesContractRepository _futuresContractRepository =
        futuresContractRepository;

    public async Task Handle(
        CreateFuturesContractCommand request,
        CancellationToken cancellationToken
    )
    {
        var contractSpecification =
            await _contractSpecificationRepository.GetContractSpecificationAsync(
                new ContractSpecificationId(request.ContractSpecificationId)
            );
        var futuresContract = new FuturesContract(contractSpecification, request.ExpirationDate);

        await _futuresContractRepository.AddAsync(futuresContract);
        await _unitOfWork.CommitAsync();
    }
}
