namespace Deopeia.Common.Localization;

public class LocalizableMessageException : Exception
{
    public LocalizableMessageException(string code)
    {
        Messages.Add(new LocalizableMessage(code));
    }

    public LocalizableMessageException(string code, object argument)
    {
        Messages.Add(new LocalizableMessage(code, argument));
    }

    public LocalizableMessageException(IEnumerable<LocalizableMessage> messages)
    {
        Messages.AddRange(messages);
    }

    public List<LocalizableMessage> Messages = [];
}
