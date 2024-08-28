namespace Deopeia.Common.Application.Option;

public record OptionsQuery<TValue> : IRequest<ICollection<OptionResult<TValue>>> { }
