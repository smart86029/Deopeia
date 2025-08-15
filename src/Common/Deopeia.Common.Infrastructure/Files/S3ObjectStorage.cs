using Amazon.S3;
using Amazon.S3.Model;

namespace Deopeia.Common.Infrastructure.Files;

internal sealed class S3ObjectStorage : IObjectStorage
{
    private readonly string _bucketName;
    private readonly bool _useHttp;
    private readonly AmazonS3Client _client;

    public S3ObjectStorage(IOptions<S3Options> options)
    {
        _bucketName = options.Value.BucketName;
        _useHttp = options.Value.ServiceUrl.StartsWith("http://");
        _client = new AmazonS3Client(
            options.Value.AccessKeyId,
            options.Value.SecretAccessKey,
            new AmazonS3Config
            {
                ServiceURL = options.Value.ServiceUrl,
                UseHttp = _useHttp,
                ForcePathStyle = true,
            }
        );
    }

    public async Task<string> GetPreSignedUrlAsync(string key, TimeSpan expiry)
    {
        var preSignedUrl = await _client.GetPreSignedURLAsync(
            new GetPreSignedUrlRequest()
            {
                BucketName = _bucketName,
                Key = key,
                Expires = DateTime.Now.Add(expiry),
            }
        );

        return _useHttp ? preSignedUrl.Replace("https://", "http://") : preSignedUrl;
    }

    public async Task UploadAsync(
        string key,
        Stream stream,
        CancellationToken cancellationToken = default
    )
    {
        await _client.PutObjectAsync(
            new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = stream,
                UseChunkEncoding = false,
            }
        );
    }

    public async Task DeleteObjectsAsync(IEnumerable<string> keys)
    {
        await _client.DeleteObjectsAsync(
            new DeleteObjectsRequest()
            {
                BucketName = _bucketName,
                Objects = keys.Select(key => new KeyVersion { Key = key }).ToList(),
            }
        );
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
