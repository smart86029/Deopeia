using System.Globalization;
using Deopeia.Common.Infrastructure.Events;
using Deopeia.Finance.Bff;
using Deopeia.Finance.Bff.Models.RealTime;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder
    .AddEventBus()
    .AddSubscription<PriceChangedEvent, PriceChangedEventHandler>()
    .AddSubscription<OrderBookChangedEvent, OrderBookChangedEventHandler>();

var services = builder.Services;
services.AddControllers();
services.AddSignalR();
services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();
services
    .AddRefitClient<IQuoteApi>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new("http://deopeia-quote-api");
        client.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentCulture.Name);
    });

services
    .AddRefitClient<ITradingApi>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new("http://deopeia-trading-api");
        client.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentCulture.Name);
    });

var app = builder.Build();
app.UseRequestLocalization("en", "zh-Hant");
app.UseHttpsRedirection();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapHub<RealTimeHub>("hub/RealTime");
app.MapReverseProxy();

app.Run();
