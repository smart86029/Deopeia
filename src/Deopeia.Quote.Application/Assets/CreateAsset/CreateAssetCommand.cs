namespace Deopeia.Quote.Application.Assets.CreateAsset;

public record CreateAssetCommand(string Code, ICollection<AssetLocaleDto> Locales) : IRequest { }
