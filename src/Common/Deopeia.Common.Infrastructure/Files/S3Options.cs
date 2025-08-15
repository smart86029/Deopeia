namespace Deopeia.Common.Infrastructure.Files;

internal class S3Options
{
    public string BucketName { get; set; } = string.Empty;

    public string ServiceUrl { get; set; } = string.Empty;

    public string AccessKeyId { get; set; } = string.Empty;

    public string SecretAccessKey { get; set; } = string.Empty;
}
