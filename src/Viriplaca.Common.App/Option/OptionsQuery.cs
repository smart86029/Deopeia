namespace Viriplaca.Common.App.Option;

public record OptionsQuery<TValue> : IRequest<ICollection<OptionResult<TValue>>> { }
