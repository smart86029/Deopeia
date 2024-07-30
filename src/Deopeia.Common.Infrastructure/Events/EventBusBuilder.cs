using Deopeia.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Deopeia.Common.Infrastructure.Events;

internal class EventBusBuilder(IServiceCollection services) : IEventBusBuilder
{
    public IServiceCollection Services => services;
}
