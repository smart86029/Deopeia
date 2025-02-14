using Deopeia.Common.Infrastructure.Events;
using Deopeia.Notification.Hub.Events;
using Deopeia.Notification.Hub.RealTime;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddServiceDefaults()
    .AddEventConsumer<CandleChangedEvent, CandleChangedEventHandler>()
    .AddEventConsumer<DealCreatedEvent, DealCreatedEventHandler>();

var services = builder.Services;
services.AddSignalR();

var app = builder.Build();
app.UseRequestLocalization("en", "zh-Hant");

app.MapDefaultEndpoints();
app.MapHub<RealTimeHub>("hub/RealTime");

app.Run();
