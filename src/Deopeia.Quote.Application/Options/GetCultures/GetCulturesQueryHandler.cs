namespace Deopeia.Quote.Application.Options.GetCultures;

internal class GetCulturesQueryHandler
    : IRequestHandler<GetCulturesQuery, ICollection<OptionResult<string>>>
{
    private static readonly CultureInfo[] Parents = CultureInfo
        .GetCultures(CultureTypes.NeutralCultures)
        .Where(x => !x.Parent.Name.IsNullOrWhiteSpace())
        .Select(x => x.Parent)
        .Distinct()
        .ToArray();

    public async Task<ICollection<OptionResult<string>>> Handle(
        GetCulturesQuery request,
        CancellationToken cancellationToken
    )
    {
        await Task.CompletedTask;

        var results = CultureInfo
            .GetCultures(CultureTypes.NeutralCultures)
            .Where(x => x.IsNeutralCulture && !Parents.Contains(x))
            .Select(x => new OptionResult<string>(x.DisplayName, x.Name))
            .ToList();

        return results;
    }
}
