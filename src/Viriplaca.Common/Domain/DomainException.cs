using Viriplaca.Common.Localization;

namespace Viriplaca.Common.Domain;

public class DomainException : LocalizedMessageException
{
    public DomainException(string code)
        : base(code)
    {
    }

    public DomainException(string code, object argument)
        : base(code, argument)
    {
    }

    public DomainException(IEnumerable<LocalizedMessage> messages)
        : base(messages)
    {
    }
}
