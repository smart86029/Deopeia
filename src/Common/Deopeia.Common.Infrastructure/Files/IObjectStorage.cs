namespace Deopeia.Common.Infrastructure.Files;

public interface IObjectStorage : IDisposable
{
    Task<string> GetPreSignedUrlAsync(string key, TimeSpan expiry);

    Task UploadAsync(string key, Stream stream, CancellationToken cancellationToken = default);

    Task DeleteObjectsAsync(IEnumerable<string> keys);
}
