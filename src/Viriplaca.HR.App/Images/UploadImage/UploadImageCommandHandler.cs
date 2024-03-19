namespace Viriplaca.HR.App.Images.UploadImage;

internal class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, UploadImageDto>
{
    public async Task<UploadImageDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = new UploadImageDto
        {
            Id = Guid.NewGuid(),
            Url = string.Empty,
        };

        return result;
    }
}
