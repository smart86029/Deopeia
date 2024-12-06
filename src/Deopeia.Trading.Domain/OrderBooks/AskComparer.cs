namespace Deopeia.Trading.Domain.OrderBooks;

internal class AskComparer : IComparer<OrderPriority>
{
    public int Compare(OrderPriority x, OrderPriority y)
    {
        var price = x.Price.CompareTo(y.Price);
        if (price != 0)
        {
            return price;
        }

        return x.CreatedAt.CompareTo(y.CreatedAt);
    }
}
