namespace Deopeia.Common.Domain;

public class AccessDeniedException : LocalizableMessageException
{
    public AccessDeniedException()
        : base("AccessDenied") { }
}
