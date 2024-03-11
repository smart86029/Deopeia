using Serilog;
using System.Reflection;
using Viriplaca.Common.Api;
using Viriplaca.Common.Data;
using Viriplaca.HR.Data;

var builder = WebApplication.CreateBuilder(args);

try
{
    var configuration = builder.Configuration;
    var services = builder.Services;
    services.AddControllers();
    services.AddApi();
    services.AddData<HRContext, HRSeeder>(Assembly.Load("Viriplaca.HR.Data"));

    var app = builder.Build();
    app.UseRequestLocalization("en-US", "zh-TW");
    app.UseAuthorization();

    app.MapControllers();

    Log.Information($"Application has started.");
    app.Run();

    return 0;
}
catch (Exception exception)
{
    Log.Fatal(exception, "Application terminated unexpectedly.");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
