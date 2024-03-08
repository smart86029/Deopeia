using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Viriplaca.Common.Data.Localization;

internal class StringLocalizerFactory<TContext>(
    IOptions<LocalizationOptions> options,
    IServiceProvider serviceProvider)
    : IStringLocalizerFactory
    where TContext : DbContext
{
    private readonly Dictionary<string, IStringLocalizer> _localizers = [];
    private readonly LocalizationOptions _options = options.Value;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IStringLocalizer Create(Type resourceSource)
    {
        if (!_localizers.TryGetValue(resourceSource.Name, out var localizer))
        {
            localizer = new StringLocalizer(GetResources(), _options);
        }

        return localizer;
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        throw new NotImplementedException();
    }

    private ICollection<LocaleResource> GetResources()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        var results = context
            .Set<LocaleResource>()
            .AsNoTracking()
            .ToList();

        return results;
    }
}
