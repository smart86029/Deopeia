using Viriplaca.Common.Domain;

namespace Viriplaca.Common.Auditing;

public abstract class AuditTrail : AggregateRoot
{
    protected AuditTrail()
    {
    }

    protected AuditTrail(AuditTrailType type, Guid createdBy, IPAddress createdIp)
    {
        Type = type;
        CreatedBy = createdBy;
        CreatedIp = createdIp;
    }

    public AuditTrailType Type { get; set; }

    public Guid CreatedBy { get; private init; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public IPAddress CreatedIp { get; init; } = IPAddress.None;
}
