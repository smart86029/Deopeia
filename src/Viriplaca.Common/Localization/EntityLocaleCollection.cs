using System.Collections.ObjectModel;

namespace Viriplaca.Common.Localization;

public class EntityLocaleCollection<TLocale> : Collection<TLocale>
    where TLocale : EntityLocale, new()
{
    public TLocale this[CultureInfo culture]
    {
        get
        {
            var locale = this.FirstOrDefault(x => x.Culture == culture);
            if (locale is null)
            {
                locale = new TLocale { Culture = culture };
                Add(locale);
            }

            return locale;
        }
        set
        {
            var locale = this.FirstOrDefault(x => x.Culture == culture);
            if (locale is not null)
            {
                Remove(locale);
            }

            value.Culture = culture;
            Add(value);
        }
    }
}
