namespace Viriplaca.Common.Localization;

public class LocalizedMessage
{
    public LocalizedMessage(string code)
    {
        Code = code;
    }

    public LocalizedMessage(string code, object argument)
    {
        Code = code;
        Argument = argument;
    }

    public string Code { get; private init; }

    public object Argument { get; private init; } = new();
}
