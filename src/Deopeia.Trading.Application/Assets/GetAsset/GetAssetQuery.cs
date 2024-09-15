namespace Deopeia.Trading.Application.Assets.GetAsset;

public record GetAssetQuery(Guid Id) : IRequest<GetAssetViewModel> { }
