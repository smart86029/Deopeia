namespace Deopeia.Common.Domain;

public class AccessDeniedException : DomainException
{
    public AccessDeniedException()
        : base("AccessDenied") { }
}
