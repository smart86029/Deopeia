using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Application.Exchanges.CreateExchange;

public class CreateExchangeCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IExchangeRepository exchangeRepository
) : IRequestHandler<CreateExchangeCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IExchangeRepository _exchangeRepository = exchangeRepository;

    public async Task Handle(CreateExchangeCommand request, CancellationToken cancellationToken)
    {
        var exchange = new Exchange(
            request.Code,
            string.Empty,
            TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone),
            request.OpeningTime,
            request.ClosingTime
        );
        foreach (var locale in request.Locales)
        {
            exchange.UpdateName(locale.Name, CultureInfo.GetCultureInfo(locale.Culture));
        }

        await _exchangeRepository.AddAsync(exchange);
        await _unitOfWork.CommitAsync();
    }
}
