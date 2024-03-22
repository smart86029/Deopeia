namespace Viriplaca.HR.App.Images.UploadImage;

public record UploadImageCommand(string FileName, byte[] Bytes)
    : IRequest<UploadImageDto>
{
}
