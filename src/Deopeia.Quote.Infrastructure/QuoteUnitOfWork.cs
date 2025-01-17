using Deopeia.Common.Events;
using Deopeia.Quote.Domain;

namespace Deopeia.Quote.Infrastructure;

public class QuoteUnitOfWork(
    QuoteContext context,
    IEventProducer eventProducer,
    CurrentUser currentUser
) : UnitOfWork<QuoteContext>(context, eventProducer, currentUser), IQuoteUnitOfWork { }
