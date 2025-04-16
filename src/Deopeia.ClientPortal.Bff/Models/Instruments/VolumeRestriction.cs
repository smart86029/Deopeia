namespace Deopeia.Finance.Bff.Models.Instruments;

public class VolumeRestriction
{
    public decimal Min { get; set; }

    public decimal Max { get; set; }

    public decimal Step { get; set; }
}
