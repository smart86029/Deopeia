using Deopeia.Quote.Domain.Assets;

namespace Deopeia.Quote.Application.Assets.UpdateAsset;

public class UpdateAssetCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IAssetRepository assetRepository
) : IRequestHandler<UpdateAssetCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAssetRepository _assetRepository = assetRepository;

    public async Task Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _assetRepository.GetAssetAsync(new AssetId(request.Id));

        var removed = asset
            .Locales.Where(x => !request.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        asset.RemoveLocales(removed);

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            asset.UpdateName(locale.Name, culture);
            asset.UpdateDescription(locale.Description, culture);
        }

        await _unitOfWork.CommitAsync();
    }
}
