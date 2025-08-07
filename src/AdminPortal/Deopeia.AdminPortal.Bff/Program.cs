var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults().AddBff();

builder.Services.AddGrpcClient<User.UserClient>(options =>
    options.Address = new Uri("http://deopeia-identity-api")
);

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapControllers();
app.MapScalar();

app.Run();
