using Viriplaca.Common.Files;

namespace Viriplaca.HR.App.Images.UploadImage;

internal class UploadImageCommandHandler(
    IHRUnitOfWork unitOfWork,
    IImageRepository imageRepository)
    : IRequestHandler<UploadImageCommand, UploadImageDto>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly IImageRepository _imageRepository = imageRepository;

    public async Task<UploadImageDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var image = new Image(request.FileName, request.Bytes);
        await _imageRepository.AddAsync(image);
        await _unitOfWork.CommitAsync();

        var result = new UploadImageDto
        {
            Id = image.Id,
            Url = image.PresignedUri?.ToString() ?? string.Empty,
        };

        return result;
    }
}
