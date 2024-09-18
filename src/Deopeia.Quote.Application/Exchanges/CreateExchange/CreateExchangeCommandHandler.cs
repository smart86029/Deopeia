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
        var en = request.Locales.First(x => x.Culture == "en");
        var exchange = new Exchange(
            request.Mic,
            en.Name,
            en.Acronym,
            TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone)
        );

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            exchange.UpdateName(locale.Name, culture);
            exchange.UpdateAcronym(locale.Acronym, culture);
        }

        await _exchangeRepository.AddAsync(exchange);
        await _unitOfWork.CommitAsync();
    }
}
