namespace Viriplaca.Common.Localization;

public class LocalizedMessageException : Exception
{
    public LocalizedMessageException(string code)
    {
        Messages.Add(new LocalizedMessage(code));
    }

    public LocalizedMessageException(string code, object argument)
    {
        Messages.Add(new LocalizedMessage(code, argument));
    }

    public LocalizedMessageException(IEnumerable<LocalizedMessage> messages)
    {
        Messages.AddRange(messages);
    }

    public List<LocalizedMessage> Messages = [];
}
