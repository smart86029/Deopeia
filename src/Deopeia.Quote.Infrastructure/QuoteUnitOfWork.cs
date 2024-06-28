using Deopeia.Quote.Domain;

namespace Deopeia.Quote.Infrastructure;

public class QuoteUnitOfWork(QuoteContext context, CurrentUser currentUser)
    : UnitOfWork<QuoteContext>(context, currentUser),
        IQuoteUnitOfWork { }
