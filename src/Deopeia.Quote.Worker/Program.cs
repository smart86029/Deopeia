using Coravel;
using Deopeia.Common;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Quote.Infrastructure;
using Deopeia.Quote.Worker;
//var builder = Host.CreateApplicationBuilder(args);
//builder.AddServiceDefaults().AddApplication().AddInfrastructure<QuoteContext>();

////builder.Services.AddHostedService<Worker>();
//builder.Services.AddScheduler();
//builder.Services.AddScoped<ScrapeJob>();
//builder.Services.AddScoped<CurrentUser>();
//builder.Services.AddScrapers();

//var host = builder.Build();
//host.Services.UseScheduler(scheduler =>
//{
//    scheduler.Schedule<ScrapeJob>().EveryMinute();
//});

//host.Run();

using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddApplication().AddInfrastructure<QuoteContext>();
builder.Services.AddScheduler();
builder.Services.AddScoped<ScrapeJob>();
builder.Services.AddScoped<CurrentUser>();
builder.Services.AddScrapers();

var app = builder.Build();
app.MapDefaultEndpoints();
app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<ScrapeJob>().EveryMinute().PreventOverlapping(nameof(ScrapeJob));
});

app.Run();
