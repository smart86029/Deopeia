using Deopeia.Common.Domain.Files;
using Minio;
using Minio.DataModel.Args;

namespace Deopeia.Common.Infrastructure.Files;

internal class ImageRepository<TContext>(TContext context, IMinioClient client) : IImageRepository
    where TContext : DbContext
{
    private readonly DbSet<Image> _images = context.Set<Image>();
    private readonly IMinioClient _client = client;

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
        var args = new PutObjectArgs()
            .WithBucket(image.BucketName)
            .WithObject(image.FileName)
            .WithObjectSize(image.Size)
            .WithStreamData(memoryStream);
        _ = await _client.PutObjectAsync(args);

        await SetPresignedUrlAsync(image);
        _images.Add(image);
    }

    private async Task SetPresignedUrlAsync(Image image)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(image.BucketName)
            .WithObject(image.FileName)
            .WithExpiry(TimeSpan.FromHours(1).TotalSeconds.ToInt());
        var presignedUri = new Uri(await _client.PresignedGetObjectAsync(args));
        image.SetPresignedUri(presignedUri);
    }
}
