namespace Viriplaca.HR.Domain;

public record WorkingTime
{
    public WorkingTime()
    {
    }

    public WorkingTime(decimal amount)
    {
        Amount = amount;
    }

    public const decimal HoursPerDay = 8;

    public decimal Amount { get; private init; }

    public int Days => (Amount / HoursPerDay).ToInt();

    public decimal Hours => (Amount % HoursPerDay).ToInt();

    public static WorkingTime FromDays(int days)
    {
        return new WorkingTime(days * HoursPerDay);
    }
}
