using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Deopeia.Common.Application.Validation;

internal class ValidationConfiguration<TOptions>(IStringLocalizer localizer)
    : IConfigureOptions<TOptions>
    where TOptions : class
{
    private readonly IStringLocalizer _localizer = localizer;

    public void Configure(TOptions options)
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) =>
            _localizer.GetPropertyString(type, member.Name);
    }
}
