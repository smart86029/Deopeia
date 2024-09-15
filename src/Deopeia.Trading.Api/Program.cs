using Deopeia.Common.Api;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Common.Infrastructure.Events;
using Deopeia.Trading.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddServiceDefaults()
    .AddApi()
    .AddApplication()
    .AddInfrastructure<TradingContext, TradingSeeder>()
    .AddEventBus("eventbus");

var configuration = builder.Configuration;
var services = builder.Services;
services.AddRazorPages();
services.AddControllers();
services.AddAuthentication().AddCookie();

var app = builder.Build();
app.UseRequestLocalization("en", "zh-Hant");
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultEndpoints();
app.MapRazorPages();
app.MapControllers();

app.Run();
