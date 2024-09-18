using Deopeia.Common.Api;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Common.Infrastructure.Events;
using Deopeia.Quote.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddServiceDefaults()
    .AddApi()
    .AddApplication()
    .AddInfrastructure<QuoteContext, QuoteSeeder>()
    .AddEventBus("eventbus");

var configuration = builder.Configuration;
var services = builder.Services;
services.AddRazorPages();
services.AddControllers();
services.AddScrapers();

services.AddAuthentication().AddCookie();

var app = builder.Build();
app.UseRequestLocalization("en", "zh-Hant");
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultEndpoints();
app.MapControllers();

app.Run();
