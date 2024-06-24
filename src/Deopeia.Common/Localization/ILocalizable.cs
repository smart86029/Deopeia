namespace Deopeia.Common.Localization;

public interface ILocalizable<TLocale>
    where TLocale : EntityLocale
{
    IReadOnlyCollection<TLocale> Locales { get; }
}
