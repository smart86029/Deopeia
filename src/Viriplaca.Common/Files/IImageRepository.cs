using Viriplaca.Common.Domain;

namespace Viriplaca.Common.Files;

public interface IImageRepository : IRepository<Image>
{
    Task<ICollection<Image>> GetImagesAsync(IEnumerable<Guid> imageIds);

    Task<Image> GetImageAsync(Guid imageId);

    Task AddAsync(Image image);
}
