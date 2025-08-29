using Deopeia.Common.Domain.Files;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.GetAvatar;

internal class GetAvatarQueryHandler(
    IUserRepository userRepository,
    IImageRepository imageRepository
) : IQueryHandler<GetAvatarQuery, Uri?>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IImageRepository _imageRepository = imageRepository;

    public async ValueTask<Uri?> Handle(GetAvatarQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(new UserId(request.UserId));
        if (user.AvatarId is null)
        {
            return null;
        }

        var avatar = await _imageRepository.GetImageAsync(user.AvatarId.Value);

        return avatar.PresignedUri;
    }
}
