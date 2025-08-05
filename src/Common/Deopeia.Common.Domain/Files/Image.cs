namespace Deopeia.Common.Domain.Files;

public class Image : FileResource
{
    private Image() { }

    public Image(string fileName, byte[] content)
        : base(FileResourceType.Image, fileName, content) { }
}
