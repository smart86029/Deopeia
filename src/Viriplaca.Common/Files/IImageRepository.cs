using Viriplaca.Common.Domain;

namespace Viriplaca.Common.Files;

public interface IImageRepository : IRepository<Image>
{
    Task<ICollection<Image>> GetImagesAsync(IEnumerable<Guid> imageIds);

    Task AddAsync(Image image);
}
