namespace Deopeia.Quote.Application.Options.GetTimeZones;

internal class GetTimeZonesQueryHandler
    : IRequestHandler<GetTimeZonesQuery, ICollection<OptionResult<string>>>
{
    private static readonly string[] DeprecatedIds =
    [
        "Mid-Atlantic Standard Time",
        "Kamchatka Standard Time"
    ];

    public async Task<ICollection<OptionResult<string>>> Handle(
        GetTimeZonesQuery request,
        CancellationToken cancellationToken
    )
    {
        await Task.CompletedTask;
        var results = TimeZoneInfo
            .GetSystemTimeZones()
            .Where(x => !DeprecatedIds.Any(y => y == x.Id))
            .Select(x => new OptionResult<string>(x.DisplayName, x.Id))
            .ToList();

        return results;
    }
}
