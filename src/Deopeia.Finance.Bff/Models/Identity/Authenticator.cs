namespace Deopeia.Finance.Bff.Models.Identity;

public class Authenticator
{
    public bool IsBound { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string ManualEntryKey { get; set; } = string.Empty;
}
