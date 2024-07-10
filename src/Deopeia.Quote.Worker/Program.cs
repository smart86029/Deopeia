using Coravel;
using Deopeia.Common;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Quote.Infrastructure;
using Deopeia.Quote.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults().AddApplication().AddInfrastructure<QuoteContext>();

builder.Services.AddScheduler();
builder.Services.AddScoped<ScrapeJob>();
builder.Services.AddScoped<CurrentUser>();
builder.Services.AddScrapers();

var host = builder.Build();
host.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<ScrapeJob>().EveryMinute().PreventOverlapping(nameof(ScrapeJob));
});

host.Run();
