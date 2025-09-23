using Deopeia.AdminPortal.Bff;
using Deopeia.Identity.Contracts;
using Deopeia.Product.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddBff();

var services = builder.Services;
services
    .AddGrpcIdentity<RoleService.RoleServiceClient>()
    .AddGrpcIdentity<UserService.UserServiceClient>()
    .AddGrpcIdentity<PermissionService.PermissionServiceClient>()
    .AddGrpcProduct<InstrumentService.InstrumentServiceClient>();

var app = builder.Build();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapScalar();

app.Run();
