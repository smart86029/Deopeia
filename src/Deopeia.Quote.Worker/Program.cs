using Coravel;
using Deopeia.Common;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Common.Infrastructure.Events;
using Deopeia.Quote.Application.Candles.MockRealTimeData;
using Deopeia.Quote.Infrastructure;
using Deopeia.Quote.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults().AddApplication().AddInfrastructure<QuoteContext>().AddEventBus();

builder.Services.AddScheduler();
builder.Services.AddScoped<ScrapeJob>();
builder.Services.AddScoped<InstrumentJob>();
builder.Services.AddScoped<ScrapeRealTimeQuoteJob>();
builder.Services.AddScoped<Job<MockRealTimeDataCommand>>();
builder.Services.AddScoped<CurrentUser>();
builder.Services.AddScrapers();

var host = builder.Build();
host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<Job<MockRealTimeDataCommand>>()
        .EveryFiveSeconds()
        .PreventOverlapping(nameof(MockRealTimeDataCommand));
    //scheduler.Schedule<ScrapeJob>().EveryMinute().PreventOverlapping(nameof(ScrapeJob));
    //scheduler.Schedule<InstrumentJob>().EveryMinute().PreventOverlapping(nameof(InstrumentJob));
    //scheduler
    //    .Schedule<ScrapeRealTimeQuoteJob>()
    //    .EveryFiveSeconds()
    //    .PreventOverlapping(nameof(ScrapeRealTimeQuoteJob));
});

host.Run();
