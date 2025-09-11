namespace Deopeia.Identity.Application.Users.UploadAvatar;

public sealed record UploadAvatarCommand(Guid Id, string FileName, byte[] Content) : ICommand;
