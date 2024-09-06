using Deopeia.Common.Infrastructure.Events;
using Deopeia.Finance.Bff;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddEventBus("eventbus").AddSubscription<PriceChangedEvent, PriceChangedEventHandler>();

var services = builder.Services;
services.AddControllers();
services.AddSignalR();
services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

var app = builder.Build();
app.UseRequestLocalization("en", "zh-Hant");
app.UseHttpsRedirection();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapHub<RealTimeHub>("hub/RealTime");
app.MapReverseProxy();

app.Run();
