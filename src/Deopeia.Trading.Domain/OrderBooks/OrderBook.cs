using Deopeia.Common.Extensions;
using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Orders;
using Deopeia.Trading.Domain.Orders.LimitOrders;
using Deopeia.Trading.Domain.Orders.MarketOrders;

namespace Deopeia.Trading.Domain.OrderBooks;

public class OrderBook : AggregateRoot<Symbol>
{
    private const int DepthMax = 5;

    private readonly PriorityQueue<Order, OrderPriority> _buyOrders = new(new BidComparer());
    private readonly PriorityQueue<Order, OrderPriority> _sellOrders = new(new AskComparer());

    private OrderBook() { }

    public OrderBook(Symbol symbol)
        : base(symbol) { }

    public decimal Bid => _buyOrders.TryPeek(out var buyOrder, out _) ? buyOrder.Price.Amount : 0;

    public decimal Ask =>
        _sellOrders.TryPeek(out var sellOrder, out _) ? sellOrder.Price.Amount : 0;

    public void AddOrder(
        OrderSide side,
        decimal volume,
        Money? openPrice,
        Money? stopLimitPrice,
        Money? takeProfitPrice,
        AccountId openedBy
    )
    {
        side.MustBeDefined();
        volume.MustGreaterThan(0);

        var orderId = new OrderId();
        if (openPrice.HasValue)
        {
            var order = new LimitOrder(side, volume, openPrice.Value, openedBy);
            Match(order);
            orderId = order.Id;
        }
        else
        {
            var order = new MarketOrder();
            Match(order);
            orderId = order.Id;
        }

        OrderBookChanged();

        //if (stopLimitPrice.HasValue)
        //{
        //    Match(new StopOrder(stopLimitPrice.Value, orderId));
        //}

        //if (takeProfitPrice.HasValue)
        //{
        //    Match(new StopOrder(takeProfitPrice.Value, orderId));
        //}
    }

    private void Match(Order taker)
    {
        switch (taker.Side)
        {
            case OrderSide.Buy:
                Match(taker, _sellOrders, _buyOrders, (taker, maker) => taker.Price < maker.Price);
                break;

            case OrderSide.Sell:
                Match(taker, _buyOrders, _sellOrders, (taker, maker) => taker.Price > maker.Price);
                break;
        }
    }

    private void Match(
        Order taker,
        PriorityQueue<Order, OrderPriority> makers,
        PriorityQueue<Order, OrderPriority> takers,
        Func<Order, Order, bool> compare
    )
    {
        while (true)
        {
            if (!makers.TryPeek(out var maker, out var _))
            {
                break;
            }

            if (compare.Invoke(taker, maker))
            {
                break;
            }

            var marketPrice = maker.Price;
            var volume = Math.Min(taker.UnfilledVolume, maker.Volume);

            taker.Fill(volume);
            maker.Fill(volume);

            if (maker.UnfilledVolume == 0)
            {
                // Update maker to filled
                makers.Dequeue();
            }

            var bid = _buyOrders.TryPeek(out var buyOrder, out _) ? buyOrder.Price.Amount : 0;
            var ask = _sellOrders.TryPeek(out var sellOrder, out _) ? sellOrder.Price.Amount : 0;
            AddDomainEvent(new DealCreatedEvent(Id.Value, marketPrice.Amount, volume, bid, ask));

            if (taker.UnfilledVolume == 0)
            {
                // Update taker to filled
                break;
            }
        }

        if (taker.UnfilledVolume > 0)
        {
            // Update taker
            takers.Enqueue(taker, new OrderPriority(taker.Price.Amount, taker.CreatedAt));
        }
    }

    private void OrderBookChanged()
    {
        var buyOrders = _buyOrders
            .UnorderedItems.GroupBy(x => x.Priority.Price)
            .OrderBy(x => x.Key)
            .TakeLast(DepthMax)
            .Select(x => new OrderDto
            {
                Price = x.Key,
                Size = x.Sum(y => y.Element.Volume).ToInt(),
            })
            .ToArray();
        var sellOrders = _sellOrders
            .UnorderedItems.GroupBy(x => x.Priority.Price)
            .OrderByDescending(x => x.Key)
            .Take(DepthMax)
            .Select(x => new OrderDto
            {
                Price = x.Key,
                Size = x.Sum(y => y.Element.Volume).ToInt(),
            })
            .ToArray();

        AddDomainEvent(new OrderBookChangedEvent(Id.Value, buyOrders, sellOrders));
    }
}
