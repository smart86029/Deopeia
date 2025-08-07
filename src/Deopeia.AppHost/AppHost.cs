using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var identityApi = builder.AddProject<Deopeia_Identity_Api>("deopeia-identity-api");

builder.AddProject<Deopeia_AdminPortal_Bff>("deopeia-adminportal-bff").WithReference(identityApi);

builder.Build().Run();
