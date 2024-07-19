using System.Reflection;
using Deopeia.Quote.Application.Instruments.ScrapeInstruments;
using Deopeia.Quote.Application.Ohlcvs.ScrapeHistoricalData;
using Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;
using Microsoft.Extensions.DependencyInjection;

namespace Deopeia.Quote.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScrapers(this IServiceCollection services)
    {
        var types = Assembly
            .GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.Name!.StartsWith("Deopeia.") && x.Name.EndsWith(".Infrastructure"))
            .Select(Assembly.Load)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType)
            .Where(x =>
                x.IsAssignableTo(typeof(IScraper))
                || x.IsAssignableTo(typeof(IRealTimeScraper))
                || x.IsAssignableTo(typeof(IInstrumentsScraper))
            );
        foreach (var type in types)
        {
            var interfaceTypes = type.GetInterfaces();
            foreach (var interfaceType in interfaceTypes)
            {
                services.AddScoped(interfaceType, type);
            }
        }

        return services;
    }
}
