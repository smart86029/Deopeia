using Deopeia.Common.Infrastructure.Events;
using Deopeia.Finance.Bff;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddEventBus("eventbus").AddSubscription<PriceChangedEvent, PriceChangedEventHandler>();

builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapControllers();
app.MapHub<RealTimeHub>("hub/RealTime");

app.Run();
