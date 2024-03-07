namespace Viriplaca.Common.App.Option;

public abstract class EnumOptionsQueryHandler<TQuery, TEnum> : IRequestHandler<TQuery, ICollection<OptionResult<TEnum>>>
    where TQuery : OptionsQuery<TEnum>
    where TEnum : Enum
{
    public async Task<ICollection<OptionResult<TEnum>>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var results = Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(@enum =>
            {
                return new OptionResult<TEnum>(@enum.ToString(), @enum);
            })
            .ToList();

        return results;
    }
}
