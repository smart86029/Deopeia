using System.Globalization;
using Deopeia.ClientPortal.Bff.Models.Quotes;
using Deopeia.Common.Bff;
using Deopeia.Finance.Bff.Models.Identity;
using Deopeia.Finance.Bff.Models.Trading;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults().AddBff();

var services = builder.Services;

services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

services
    .AddRefitClient<IIdentityApi>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new("http://deopeia-identity-api");
        client.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentCulture.Name);
    });

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
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapReverseProxy();

app.Run();
