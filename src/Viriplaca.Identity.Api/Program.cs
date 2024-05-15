using Serilog;
using Viriplaca.Common;
using Viriplaca.Common.Api;
using Viriplaca.Common.Data;
using Viriplaca.Identity.Api;
using Viriplaca.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

try
{
    var configuration = builder.Configuration;
    var services = builder.Services;
    services.AddRazorPages();
    services.AddControllers();
    services.AddApi();
    services.AddData<IdentityContext, IdentitySeeder>(
        configuration.GetSection("MinIO").Get<MinIOOptions>()!
    );
    services.AddAuthentication().AddCookie();
    services.AddCors(options =>
    {
        options.AddPolicy(
            CorsPolicies.Oidc,
            policy =>
            {
                policy.AllowAnyOrigin();
            }
        );
    });

    var app = builder.Build();
    app.UseExceptionHandler();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCors();
    app.UseRequestLocalization("en-US", "zh-TW");
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();
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
