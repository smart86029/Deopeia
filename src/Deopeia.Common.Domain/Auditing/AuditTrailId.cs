namespace Deopeia.Common.Domain.Auditing;

public readonly record struct AuditTrailId(Guid Guid) : IEntityId
{
    public AuditTrailId()
        : this(GuidUtility.NewGuid()) { }
}
