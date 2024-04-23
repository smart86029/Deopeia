namespace Viriplaca.Common.Auditing;

public enum AuditTrailType
{
    Unknown = 0,

    AdminActivity = 1,

    DataAccess = 2,

    PolicyDenied = 3,

    SystemEvent = 4,
}
