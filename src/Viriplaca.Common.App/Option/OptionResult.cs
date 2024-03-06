namespace Viriplaca.Common.App.Option;

public class OptionResult<TValue>
{
    public OptionResult()
    {
    }

    public OptionResult(string name, TValue value)
        : this(name, value, true)
    {
    }

    public OptionResult(string name, TValue value, bool isEnabled)
    {
        Name = name;
        Value = value;
        IsEnabled = isEnabled;
    }

    public string Name { get; private set; } = string.Empty;

    public TValue Value { get; private set; } = default!;

    public bool IsEnabled { get; private set; } = true;
}
