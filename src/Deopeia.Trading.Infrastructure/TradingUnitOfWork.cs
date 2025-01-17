using Deopeia.Common.Events;

namespace Deopeia.Trading.Infrastructure;

public class TradingUnitOfWork(
    TradingContext context,
    IEventProducer eventProducer,
    CurrentUser currentUser
) : UnitOfWork<TradingContext>(context, eventProducer, currentUser), ITradingUnitOfWork { }
