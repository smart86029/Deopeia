namespace Deopeia.Common.Application.Option;

public class OptionResult<TValue>
{
    public OptionResult()
        : this(string.Empty, default!) { }

    public OptionResult(string name, TValue value)
        : this(name, value, true) { }

    public OptionResult(string name, TValue value, bool isEnabled)
    {
        Name = name;
        Value = value;
        IsEnabled = isEnabled;
    }

    public string Name { get; private set; }

    public TValue Value { get; private set; }

    public bool IsEnabled { get; private set; }
}
