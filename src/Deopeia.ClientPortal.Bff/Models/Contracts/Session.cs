namespace Deopeia.Finance.Bff.Models.Contracts;

public class Session
{
    public DayOfWeek OpenDay { get; set; }

    public TimeOnly OpenTime { get; set; }

    public DayOfWeek CloseDay { get; set; }

    public TimeOnly CloseTime { get; set; }
}
