namespace Deopeia.Common.Bff.Option;

public record OptionResponse<TValue>(string Name, TValue Value, bool IsEnabled)
{
    public OptionResponse(string name, TValue value)
        : this(name, value, true) { }
}
