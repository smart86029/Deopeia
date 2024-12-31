using Coravel;
using Deopeia.Common;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Common.Infrastructure.Events;
using Deopeia.Common.Worker;
using Deopeia.Trading.Application.Orders.MockOrders;
using Deopeia.Trading.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder
    .AddServiceDefaults()
    .AddApplication()
    .AddInfrastructure<TradingContext, TradingSeeder>()
    .AddEventBus();

builder.Services.AddScheduler();
builder.Services.AddScoped<Job<MockOrdersCommand>>();
builder.Services.AddScoped<CurrentUser>();

var host = builder.Build();
host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<Job<MockOrdersCommand>>()
        .EverySecond()
        .PreventOverlapping(nameof(MockOrdersCommand));
});

host.Run();
