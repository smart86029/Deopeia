using Deopeia.Quote.Application.Exchanges.CreateExchange;
using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Application.FuturesContracts.CreateFuturesContract;

public class CreateFuturesContractCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IExchangeRepository exchangeRepository
) : IRequestHandler<CreateFuturesContractCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IExchangeRepository _exchangeRepository = exchangeRepository;

    public async Task Handle(
        CreateFuturesContractCommand request,
        CancellationToken cancellationToken
    )
    {
        //var en = request.Locales.First(x => x.Culture == "en");
        //var exchange = new Exchange(
        //    request.Mic,
        //    en.Name,
        //    en.Abbreviation,
        //    TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone)
        //);

        //foreach (var locale in request.Locales)
        //{
        //    var culture = CultureInfo.GetCultureInfo(locale.Culture);
        //    exchange.UpdateName(locale.Name, culture);
        //    exchange.UpdateAbbreviation(locale.Abbreviation, culture);
        //}

        //await _exchangeRepository.AddAsync(exchange);
        await _unitOfWork.CommitAsync();
    }
}
