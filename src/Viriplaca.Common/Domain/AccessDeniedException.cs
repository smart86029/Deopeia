using Viriplaca.Common.Localization;

namespace Viriplaca.Common.Domain;

public class AccessDeniedException : LocalizableMessageException
{
    public AccessDeniedException()
        : base("AccessDenied")
    {
    }
}
