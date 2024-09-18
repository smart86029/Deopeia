namespace Deopeia.Quote.Application.Assets.UpdateAsset;

public record UpdateAssetCommand(Guid Id, string Code, ICollection<AssetLocaleDto> Locales)
    : IRequest { }
