namespace Deopeia.Identity.Application.Users.UploadAvatar;

public record UploadAvatarCommand(Guid Id, string FileName, byte[] Content) : ICommand;
