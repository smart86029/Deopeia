using System.Text;
using Deopeia.Common;
using Deopeia.Common.Infrastructure.Events;
using Deopeia.Finance.Bff;
using Deopeia.Finance.Bff.Models.RealTime;
using Deopeia.Finance.Bff.Models.Trading;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder
    .AddEventBus()
    .AddSubscription<DealCreatedEvent, DealCreatedEventHandler>()
    .AddSubscription<CandleChangedEvent, CandleChangedEventHandler>()
    .AddSubscription<OrderBookChangedEvent, OrderBookChangedEventHandler>();

var services = builder.Services;
services.AddControllers();
services.AddSignalR();

var jwtOptions = new JwtOptions();
builder.Configuration.Bind("Jwt", jwtOptions);
services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:7099/";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
        };

        var handler = new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
        };
        options.Backchannel = new HttpClient(handler);
    });

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
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapHub<RealTimeHub>("hub/RealTime");
app.MapReverseProxy();

app.Run();
