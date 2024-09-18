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
        var exchange = await _exchangeRepository.GetExchangeAsync(new ExchangeId(request.Mic));

        var removed = exchange
            .Locales.Where(x => !request.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        exchange.RemoveLocales(removed);

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            exchange.UpdateName(locale.Name, culture);
            exchange.UpdateAcronym(locale.Acronym, culture);
        }

        await _unitOfWork.CommitAsync();
    }
}
