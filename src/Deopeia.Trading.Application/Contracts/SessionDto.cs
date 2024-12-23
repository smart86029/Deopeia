namespace Deopeia.Trading.Application.Contracts;

public class SessionDto
{
    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly OpenTime { get; set; }

    public TimeOnly CloseTime { get; set; }
}
