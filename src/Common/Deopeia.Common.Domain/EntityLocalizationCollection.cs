namespace Deopeia.Common.Domain;

public class EntityLocalizationCollection<TLocalization, TEntityId> : List<TLocalization>
    where TLocalization : EntityLocalization<TEntityId>, new()
    where TEntityId : struct, IEntityId
{
    public TLocalization this[CultureInfo culture]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(culture);

            var localization = this.FirstOrDefault(x => x.Culture.Equals(culture));
            if (localization is null)
            {
                localization = new TLocalization { Culture = culture };
                Add(localization);
            }

            return localization;
        }
        set
        {
            ArgumentNullException.ThrowIfNull(culture);
            ArgumentNullException.ThrowIfNull(value);

            var index = FindIndex(x => x.Culture.Equals(culture));
            if (index < 0)
            {
                value.Culture = culture;
                Add(value);
            }
            else
            {
                value.Culture = culture;
                this[index] = value;
            }
        }
    }

    public TLocalization Default => this[CultureInfo.DefaultThreadCurrentCulture!];

    public void RemoveRange(IEnumerable<TLocalization> localizations)
    {
        ArgumentNullException.ThrowIfNull(localizations);

        foreach (var localization in localizations)
        {
            Remove(localization);
        }
    }
}
