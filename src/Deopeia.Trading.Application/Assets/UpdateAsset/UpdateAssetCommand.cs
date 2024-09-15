namespace Deopeia.Trading.Application.Assets.UpdateAsset;

public record UpdateAssetCommand(Guid Id, string Code, ICollection<AssetLocaleDto> Locales)
    : IRequest { }
