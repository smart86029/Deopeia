namespace Deopeia.Common.Domain.Files;

public interface IImageRepository : IRepository<Image, FileResourceId>
{
    Task<ICollection<Image>> GetImagesAsync(IEnumerable<FileResourceId> imageIds);

    Task<Image> GetImageAsync(FileResourceId imageId);

    Task AddAsync(Image image);
}
