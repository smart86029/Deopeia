using Coravel;
using Deopeia.Common;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Common.Infrastructure.Events;
using Deopeia.Common.Worker;
using Deopeia.Trading.Application.Orders.MockOrders;
using Deopeia.Trading.Application.Traders.TraderCreated;
using Deopeia.Trading.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder
    .AddServiceDefaults()
    .AddApplication()
    .AddInfrastructure<TradingContext, TradingSeeder>()
    .AddEventConsumer<TraderCreatedEvent, TraderCreatedEventHandler>();

builder.Services.AddScheduler();
builder.Services.AddScoped<CurrentUser>();

var host = builder.Build();
host.Services.UseScheduler(scheduler =>
{
    scheduler
        .ScheduleWithParams<Job>(new MockOrdersCommand())
        .EverySecond()
        .PreventOverlapping(nameof(MockOrdersCommand));
});

host.Run();
