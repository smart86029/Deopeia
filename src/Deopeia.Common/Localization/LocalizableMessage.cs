namespace Deopeia.Common.Localization;

public class LocalizableMessage
{
    public LocalizableMessage(string code)
    {
        Code = code;
    }

    public LocalizableMessage(string code, object argument)
    {
        Code = code;
        Argument = argument;
    }

    public string Code { get; private init; }

    public object Argument { get; private init; } = new();
}
