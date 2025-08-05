namespace Deopeia.Common.Domain.Auditing;

public abstract class AuditTrail : AggregateRoot<AuditTrailId>
{
    protected AuditTrail() { }

    protected AuditTrail(AuditTrailType type, Guid createdBy, IPAddress address)
    {
        Type = type;
        CreatedBy = createdBy;
        IPAddress = address;
    }

    public AuditTrailType Type { get; set; }

    public Guid CreatedBy { get; private init; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public IPAddress IPAddress { get; init; } = IPAddress.Any;
}
