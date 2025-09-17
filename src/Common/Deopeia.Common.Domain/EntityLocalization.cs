namespace Deopeia.Common.Domain;

public abstract class EntityLocalization<TEntityId>
    where TEntityId : struct, IEntityId
{
    protected EntityLocalization() { }

    protected EntityLocalization(TEntityId entityId, CultureInfo culture)
    {
        EntityId = entityId;
        Culture = culture;
    }

    public TEntityId EntityId { get; private set; }

    [JsonIgnore]
    public CultureInfo Culture { get; internal set; } = CultureInfo.CurrentCulture;
}
