using Deopeia.Common.Extensions;
using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Orders;
using Deopeia.Trading.Domain.Orders.LimitOrders;
using Deopeia.Trading.Domain.Orders.MarketOrders;

namespace Deopeia.Trading.Domain.MatchingEngines;

public class OrderBook : AggregateRoot<InstrumentId>
{
    private const int DepthMax = 5;

    private readonly PriorityQueue<Order, OrderPriority> _bids = new(new BidComparer());
    private readonly PriorityQueue<Order, OrderPriority> _asks = new(new AskComparer());

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
                Match(taker, _asks, _bids, (taker, maker) => taker.Price < maker.Price);
                break;

            case OrderSide.Sell:
                Match(taker, _bids, _asks, (taker, maker) => taker.Price > maker.Price);
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
        var takerUnfilled = taker.Volume;
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
            var volume = Math.Min(takerUnfilled, maker.Volume);
            AddDomainEvent(
                new PriceChangedEvent("GCZ2024", DateTimeOffset.Now, marketPrice.Amount, 2500)
            );

            takerUnfilled -= volume;

            if (maker.Volume - volume == 0)
            {
                // Update maker to filled
                makers.Dequeue();
            }
            else
            {
                // Update maker to partial filled
            }

            if (takerUnfilled == 0)
            {
                // Update taker to filled
                break;
            }
        }

        if (takerUnfilled > 0)
        {
            // Update taker
            takers.Enqueue(taker, new OrderPriority(taker.Price.Amount, taker.CreatedAt));
        }
    }

    private void OrderBookChanged()
    {
        var bids = _bids
            .UnorderedItems.GroupBy(x => x.Priority.Price)
            .OrderBy(x => x.Key)
            .Take(DepthMax)
            .Select(x => new OrderDto
            {
                Price = x.Key,
                Size = x.Sum(y => y.Element.Volume).ToInt()
            })
            .ToArray();
        var asks = _asks
            .UnorderedItems.GroupBy(x => x.Priority.Price)
            .OrderByDescending(x => x.Key)
            .Take(DepthMax)
            .Select(x => new OrderDto
            {
                Price = x.Key,
                Size = x.Sum(y => y.Element.Volume).ToInt()
            })
            .ToArray();

        AddDomainEvent(new OrderBookChangedEvent("GCZ2024", bids, asks));
    }
}