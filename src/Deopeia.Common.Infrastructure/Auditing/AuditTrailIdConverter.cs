using Deopeia.Common.Domain.Auditing;

namespace Deopeia.Common.Infrastructure.Auditing;

internal class AuditTrailIdConverter()
    : ValueConverter<AuditTrailId, Guid>(id => id.Guid, guid => new AuditTrailId(guid)) { }
