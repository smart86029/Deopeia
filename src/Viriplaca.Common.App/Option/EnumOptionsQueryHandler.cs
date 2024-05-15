using Microsoft.Extensions.Localization;

namespace Viriplaca.Common.App.Option;

public abstract class EnumOptionsQueryHandler<TQuery, TEnum>(IStringLocalizer localizer)
    : IRequestHandler<TQuery, ICollection<OptionResult<TEnum>>>
    where TQuery : OptionsQuery<TEnum>
    where TEnum : Enum
{
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<ICollection<OptionResult<TEnum>>> Handle(
        TQuery request,
        CancellationToken cancellationToken
    )
    {
        await Task.CompletedTask;
        var results = Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(@enum =>
            {
                return new OptionResult<TEnum>(_localizer.GetEnumString(@enum), @enum);
            })
            .ToList();

        return results;
    }
}
