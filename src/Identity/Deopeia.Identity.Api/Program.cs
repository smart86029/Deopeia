using Deopeia.Common;
using Deopeia.Common.Api;
using Deopeia.Common.Infrastructure;
using Deopeia.Identity.Api;
using Deopeia.Identity.Api.Services;
using Deopeia.Identity.Application;
using Deopeia.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddApi().AddApplication().AddInfrastructure();

var services = builder.Services;
var configuration = builder.Configuration;
services.AddCors(options =>
    options.AddPolicy(CorsPolicies.Oidc, policy => policy.AllowAnyOrigin().AllowAnyMethod())
);
services.AddAuthentication().AddCookie();
services.AddRazorPages();
services.AddControllers();
services.AddGrpc();

services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

var app = builder.Build();
app.UseRequestLocalization();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.Migrate<IdentityContext>();
}

app.MapDefaultEndpoints();
app.MapRazorPages();
app.MapControllers();
app.MapGrpcService<RoleService>();
app.MapGrpcService<UserService>();
app.MapGrpcService<PermissionService>();

app.Run();
