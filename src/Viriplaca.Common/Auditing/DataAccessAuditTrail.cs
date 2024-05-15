namespace Viriplaca.Common.Auditing;

public class DataAccessAuditTrail : AuditTrail
{
    private readonly Dictionary<string, object> _keys = [];
    private readonly Dictionary<string, object?> _oldValues = [];
    private readonly Dictionary<string, object?> _newValues = [];
    private readonly List<string> _propertyNames = [];

    private DataAccessAuditTrail() { }

    public DataAccessAuditTrail(
        Type entityType,
        IDictionary<string, object> keys,
        IDictionary<string, object?> oldValues,
        IDictionary<string, object?> newValues,
        IEnumerable<string> propertyNames,
        Guid createdBy,
        IPAddress createdIp
    )
        : base(AuditTrailType.DataAccess, createdBy, createdIp)
    {
        EntityType = entityType;
        _keys.AddRange(keys);
        _oldValues.AddRange(oldValues);
        _newValues.AddRange(newValues);
        _propertyNames.AddRange(propertyNames);
    }

    public Type EntityType { get; private init; } = null!;

    public IReadOnlyDictionary<string, object> Keys => _keys.AsReadOnly();

    public IReadOnlyDictionary<string, object?> OldValues => _oldValues.AsReadOnly();

    public IReadOnlyDictionary<string, object?> NewValues => _newValues.AsReadOnly();

    public IReadOnlyCollection<string> PropertyNames => _propertyNames.AsReadOnly();
}
