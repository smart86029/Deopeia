using Deopeia.Common.Domain.Files;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.UploadAvatar;

public class UploadAvatarCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IImageRepository imageRepository
) : ICommandHandler<UploadAvatarCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IImageRepository _imageRepository = imageRepository;

    public async ValueTask<Unit> Handle(
        UploadAvatarCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(command.Id));

        var avatar = new Image(command.FileName, command.Content);
        await _imageRepository.AddAsync(avatar);

        user.UpdateAvatar(avatar);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
