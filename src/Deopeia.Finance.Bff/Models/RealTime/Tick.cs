namespace Deopeia.Finance.Bff.Models.RealTime;

public record Tick(long Timestamp, decimal Price, decimal Volume, decimal Bid, decimal Ask) { }
