using Deopeia.Common;
using Deopeia.Common.Api;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Identity.Api;
using Deopeia.Identity.Api.Services.Authentication;
using Deopeia.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddServiceDefaults()
    .AddApi()
    .AddApplication()
    .AddInfrastructure<IdentityContext, IdentitySeeder>();

var configuration = builder.Configuration;
var services = builder.Services;
services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
services.AddRazorPages();
services.AddControllers();
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
services.AddScoped<AuthenticationService>();

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
    app.Migrate<IdentityContext>();
    app.UseScalar();
}

app.MapDefaultEndpoints();
app.MapRazorPages();
app.MapControllers();

app.Run();
