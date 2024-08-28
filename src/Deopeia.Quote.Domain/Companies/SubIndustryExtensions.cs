namespace Deopeia.Quote.Domain.Companies;

public static class SubIndustryExtensions
{
    public static Sector ToSector(this SubIndustry subIndustry)
    {
        return (Sector)((int)subIndustry / 1000000);
    }

    public static Industry ToIndustry(this SubIndustry subIndustry)
    {
        return (Industry)((int)subIndustry / 100);
    }
}
