namespace Deopeia.Quote.Application.Assets.GetAsset;

public class GetAssetViewModel
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public ICollection<AssetLocaleDto> Locales { get; set; } = [];
}
