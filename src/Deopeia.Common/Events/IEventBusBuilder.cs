using Microsoft.Extensions.DependencyInjection;

namespace Deopeia.Common.Events;

public interface IEventBusBuilder
{
    IServiceCollection Services { get; }
}
