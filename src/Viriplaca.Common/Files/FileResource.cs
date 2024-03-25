using Viriplaca.Common.Domain;

namespace Viriplaca.Common.Files;

public abstract class FileResource : AggregateRoot
{
    protected FileResource()
    {
    }

    protected FileResource(FileResourceType type, string fileName, byte[] content)
    {
        type.MustBeDefined();
        fileName.MustNotBeNullOrWhiteSpace();

        Type = type;
        Name = Id.ToString("N");
        Extension = Path.GetExtension(fileName);
        Content = content;
        Size = content.Length;
    }

    public FileResourceType Type { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Extension { get; private set; } = string.Empty;

    public int Size { get; private set; }

    public byte[] Content { get; private set; } = [];

    public Uri? PresignedUri { get; private set; }

    public string BucketName => Type.ToString().ToLower();

    public string FileName => $"{Name}{Extension}";

    public void SetPresignedUri(Uri presignedUri)
    {
        PresignedUri = presignedUri;
    }
}
