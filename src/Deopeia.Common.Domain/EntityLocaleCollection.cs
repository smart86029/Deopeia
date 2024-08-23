using System.Collections.ObjectModel;
using Deopeia.Common.Domain;

namespace Deopeia.Common.Localization;

public class EntityLocaleCollection<TLocale, TEntityId> : Collection<TLocale>
    where TLocale : EntityLocale<TEntityId>, new()
    where TEntityId : struct, IEntityId
{
    private static readonly CultureInfo DefaultCulture = CultureInfo.GetCultureInfo("en-US");

    public TLocale this[CultureInfo culture]
    {
        get
        {
            var locale = this.FirstOrDefault(x => x.Culture.Equals(culture));
            if (locale is null)
            {
                locale = new TLocale { Culture = culture };
                Add(locale);
            }

            return locale;
        }
        set
        {
            var locale = this.FirstOrDefault(x => x.Culture.Equals(culture));
            if (locale is not null)
            {
                Remove(locale);
            }

            value.Culture = culture;
            Add(value);
        }
    }

    public TLocale Default => this[DefaultCulture];
}
