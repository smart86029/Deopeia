using Deopeia.Quote.Domain.Assets;

namespace Deopeia.Quote.Application.Assets.CreateAsset;

public class CreateAssetCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IAssetRepository assetRepository
) : IRequestHandler<CreateAssetCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAssetRepository _assetRepository = assetRepository;

    public async Task Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var en = request.Locales.First(x => x.Culture == "en");
        var asset = new Asset(request.Code, en.Name, en.Description);
        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            asset.UpdateName(locale.Name, culture);
            asset.UpdateDescription(locale.Description, culture);
        }

        _assetRepository.Add(asset);
        await _unitOfWork.CommitAsync();
    }
}
