using Deopeia.Common.Domain.Files;

namespace Deopeia.Common.Infrastructure.Files;

internal class ImageRepository<TContext>(TContext context, IObjectStorage objectStorage)
    : IImageRepository
    where TContext : DbContext
{
    private readonly DbSet<Image> _images = context.Set<Image>();
    private readonly IObjectStorage _objectStorage = objectStorage;

    public async Task<ICollection<Image>> GetImagesAsync(IEnumerable<FileResourceId> imageIds)
    {
        var results = await _images.Where(x => imageIds.Contains(x.Id)).ToListAsync();
        foreach (var image in results)
        {
            await SetPresignedUrlAsync(image);
        }

        return results;
    }

    public async Task<Image> GetImageAsync(FileResourceId imageId)
    {
        var result = await _images.FirstAsync(x => x.Id == imageId);
        await SetPresignedUrlAsync(result);
        return result;
    }

    public async Task AddAsync(Image image)
    {
        using var memoryStream = new MemoryStream(image.Content);
        await _objectStorage.UploadAsync(image.FileName, memoryStream);
        await SetPresignedUrlAsync(image);
        _images.Add(image);
    }

    private async Task SetPresignedUrlAsync(Image image)
    {
        var presignedUri = new Uri(
            await _objectStorage.GetPreSignedUrlAsync(image.FileName, TimeSpan.FromHours(1))
        );
        image.SetPresignedUri(presignedUri);
    }
}
