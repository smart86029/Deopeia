using Deopeia.Common.Api;
using Deopeia.Common.Application;
using Deopeia.Common.Infrastructure;
using Deopeia.Identity.Api;
using Deopeia.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddServiceDefaults()
    .AddApi()
    .AddApplication()
    .AddInfrastructure<IdentityContext, IdentitySeeder>();

var configuration = builder.Configuration;
var services = builder.Services;
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

var app = builder.Build();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseRequestLocalization("en-US", "zh-TW");
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
