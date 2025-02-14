namespace Deopeia.Notification.Hub.RealTime;

public record Tick(long Timestamp, decimal Price, decimal Volume, decimal Bid, decimal Ask) { }
