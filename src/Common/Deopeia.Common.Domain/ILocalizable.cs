namespace Deopeia.Common.Domain;

public interface ILocalizable<TLocale, TEntityId>
    where TLocale : EntityLocale<TEntityId>
    where TEntityId : struct, IEntityId
{
    IReadOnlyCollection<TLocale> Locales { get; }
}
