using Deopeia.Common.Api;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Quote.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddServiceDefaults()
    .AddApi()
    .AddApplication()
    .AddInfrastructure<QuoteContext, QuoteSeeder>();

var configuration = builder.Configuration;
var services = builder.Services;
services.AddRazorPages();
services.AddControllers();
services.AddScrapers();

services.AddAuthentication().AddCookie();

var app = builder.Build();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseRequestLocalization("en-US", "zh-TW");
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapRazorPages();
app.MapControllers();

app.Run();
