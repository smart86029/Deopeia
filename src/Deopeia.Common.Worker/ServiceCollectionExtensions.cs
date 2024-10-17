using Coravel;
using Microsoft.Extensions.DependencyInjection;

namespace Deopeia.Common.Worker;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorker(this IServiceCollection services)
    {
        services.AddScheduler();

        return services;
    }
}
