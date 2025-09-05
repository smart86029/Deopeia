using Deopeia.AdminPortal.Bff;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddBff();

var services = builder.Services;
services
    .AddGrpcIdentity<RoleService.RoleServiceClient>()
    .AddGrpcIdentity<UserService.UserServiceClient>()
    .AddGrpcIdentity<PermissionService.PermissionServiceClient>();

var app = builder.Build();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapScalar();

app.Run();
