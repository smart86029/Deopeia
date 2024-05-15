using Viriplaca.Common.Localization;

namespace Viriplaca.Common.Domain;

public class DomainException : LocalizableMessageException
{
    public DomainException(string code)
        : base(code) { }

    public DomainException(string code, object argument)
        : base(code, argument) { }

    public DomainException(IEnumerable<LocalizableMessage> messages)
        : base(messages) { }
}
