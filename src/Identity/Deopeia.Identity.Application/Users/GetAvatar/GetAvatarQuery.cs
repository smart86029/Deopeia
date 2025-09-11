namespace Deopeia.Identity.Application.Users.GetAvatar;

public sealed record GetAvatarQuery(Guid UserId) : IQuery<Uri?>;
