namespace Deopeia.Finance.Bff.Models.Contracts;

public class Session
{
    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly OpenTime { get; set; }

    public TimeOnly CloseTime { get; set; }
}
