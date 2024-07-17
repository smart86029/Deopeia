namespace Deopeia.Finance.Bff;

public interface IRealTime
{
    Task ReceiveQuote(string quote);
}
