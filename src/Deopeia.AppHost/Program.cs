var builder = DistributedApplication.CreateBuilder(args);

var minIOEndpoint = builder.AddParameter("MinIOEndpoint");
var minIOAccessKey = builder.AddParameter("MinIOAccessKey");
var minIOSecretKey = builder.AddParameter("MinIOSecretKey");

var sql = builder.AddSqlServer("sql");
var dbIdentity = sql.AddDatabase("Identity");

builder
    .AddProject<Projects.Deopeia_Identity_Api>("deopeia-identity-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(dbIdentity);

builder.Build().Run();
