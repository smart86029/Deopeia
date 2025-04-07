namespace Deopeia.Identity.Application.Users.GetAvatar;

public record GetAvatarQuery(Guid UserId) : IRequest<byte[]>;
