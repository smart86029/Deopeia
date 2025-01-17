using Deopeia.Common.Events;

namespace Deopeia.Identity.Infrastructure;

public class IdentityUnitOfWork(
    IdentityContext context,
    IEventProducer eventProducer,
    CurrentUser currentUser
) : UnitOfWork<IdentityContext>(context, eventProducer, currentUser), IIdentityUnitOfWork { }
