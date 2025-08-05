namespace Deopeia.Common.Domain;

public abstract class EntityLocale<TEntityId>
    where TEntityId : struct, IEntityId
{
    protected EntityLocale() { }

    protected EntityLocale(TEntityId entityId, CultureInfo culture)
    {
        EntityId = entityId;
        Culture = culture;
    }

    public TEntityId EntityId { get; private set; }

    [JsonIgnore]
    public CultureInfo Culture { get; internal set; } = CultureInfo.CurrentCulture;
}
