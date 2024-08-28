using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Application.Exchanges.UpdateExchange;

public class UpdateExchangeCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IExchangeRepository exchangeRepository
) : IRequestHandler<UpdateExchangeCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IExchangeRepository _exchangeRepository = exchangeRepository;

    public async Task Handle(UpdateExchangeCommand request, CancellationToken cancellationToken)
    {
        var exchange = await _exchangeRepository.GetExchangeAsync(new ExchangeId(request.Id));
        exchange.UpdateOpenHours(request.OpeningTime, request.ClosingTime);

        var removed = exchange
            .Locales.Where(x => !request.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        exchange.RemoveLocales(removed);

        foreach (var locale in request.Locales)
        {
            exchange.UpdateName(locale.Name, CultureInfo.GetCultureInfo(locale.Culture));
        }

        await _unitOfWork.CommitAsync();
    }
}
