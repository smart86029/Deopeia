namespace Deopeia.AdminPortal.Bff.Models.Me;

public sealed record UpdatePasswordRequest(string CurrentPassword, string NewPassword);
