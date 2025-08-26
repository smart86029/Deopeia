namespace Deopeia.Common.Application.Option;

public record OptionsQuery<TValue> : IQuery<IReadOnlyList<OptionResult<TValue>>> { }
