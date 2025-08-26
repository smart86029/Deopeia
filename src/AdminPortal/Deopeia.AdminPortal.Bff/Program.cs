var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddBff();

builder.Services.AddGrpcClient<RoleService.RoleServiceClient>(options =>
    options.Address = new Uri("http://deopeia-identity-api")
);
builder.Services.AddGrpcClient<UserService.UserServiceClient>(options =>
    options.Address = new Uri("http://deopeia-identity-api")
);

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapScalar();

app.Run();
