namespace Deopeia.Trading.Infrastructure;

public class TradingUnitOfWork(TradingContext context, CurrentUser currentUser)
    : UnitOfWork<TradingContext>(context, currentUser),
        ITradingUnitOfWork { }
