using Deopeia.Common.Domain.Files;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.UploadAvatar;

public class UploadAvatarCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IImageRepository imageRepository
) : IRequestHandler<UploadAvatarCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IImageRepository _imageRepository = imageRepository;

    public async Task Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(new UserId(request.Id));

        var avatar = new Image(request.FileName, request.Content);
        await _imageRepository.AddAsync(avatar);

        user.UpdateAvatar(avatar);
        await _unitOfWork.CommitAsync();
    }
}
