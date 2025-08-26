namespace Deopeia.Common.Bff.Option;

public class OptionResponse<TValue>
{
    public required string Name { get; init; }

    public required TValue Value { get; set; }

    public bool IsEnabled { get; set; }
}
