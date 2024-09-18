namespace Deopeia.Quote.Application.Assets.GetAsset;

public record GetAssetQuery(Guid Id) : IRequest<GetAssetViewModel> { }
