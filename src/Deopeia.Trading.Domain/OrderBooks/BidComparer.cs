namespace Deopeia.Trading.Domain.OrderBooks;

internal class BidComparer : IComparer<OrderPriority>
{
    public int Compare(OrderPriority x, OrderPriority y)
    {
        var price = y.Price.CompareTo(x.Price);
        if (price != 0)
        {
            return price;
        }

        return y.CreatedAt.CompareTo(x.CreatedAt);
    }
}
