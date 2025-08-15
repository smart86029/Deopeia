namespace Deopeia.Common.Application.Option;

public record OptionsQuery<TValue> : IQuery<ICollection<OptionResult<TValue>>> { }
