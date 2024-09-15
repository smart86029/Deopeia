namespace Deopeia.Trading.Application.Assets.CreateAsset;

public record CreateAssetCommand(string Code, ICollection<AssetLocaleDto> Locales) : IRequest { }
