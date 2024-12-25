namespace Deopeia.Trading.Application.Contracts;

public class SessionDto
{
    public DayOfWeek OpenDay { get; set; }

    public TimeOnly OpenTime { get; set; }

    public DayOfWeek CloseDay { get; set; }

    public TimeOnly CloseTime { get; set; }
}
