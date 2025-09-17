namespace Deopeia.Common.Domain;

public interface ILocalizable<TLocalization, TEntityId>
    where TLocalization : EntityLocalization<TEntityId>
    where TEntityId : struct, IEntityId
{
    IReadOnlyList<TLocalization> Localizations { get; }
}
