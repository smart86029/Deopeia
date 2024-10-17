using Deopeia.Common.Utilities;
using Deopeia.Quote.Application.Candles.ScrapeHistoricalData;
using Deopeia.Quote.Application.Candles.ScrapeRealTimeData;
using Deopeia.Quote.Application.Instruments.ScrapeInstruments;
using Microsoft.Extensions.DependencyInjection;

namespace Deopeia.Quote.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScrapers(this IServiceCollection services)
    {
        var types = AssemblyUtility
            .GetTypes()
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
