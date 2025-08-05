using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Deopeia_Identity_Api>("deopeia-identity-api");

builder.AddProject<Deopeia_AdminPortal_Bff>("deopeia-adminportal-bff");

builder.Build().Run();
