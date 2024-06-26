var builder = DistributedApplication.CreateBuilder(args);

var minIOEndpoint = builder.AddParameter("MinIOEndpoint");
var minIOAccessKey = builder.AddParameter("MinIOAccessKey");
var minIOSecretKey = builder.AddParameter("MinIOSecretKey");

var sql = builder.AddSqlServer("sql");
var dbIdentity = sql.AddDatabase("Identity");
var dbQuote = sql.AddDatabase("Quote");

builder
    .AddProject<Projects.Deopeia_Identity_Api>("deopeia-identity-api")
    .WithEnvironment("MinIO__Endpoint", minIOEndpoint)
    .WithEnvironment("MinIO__AccessKey", minIOAccessKey)
    .WithEnvironment("MinIO__SecretKey", minIOSecretKey)
    .WithReference(dbIdentity);

builder.AddProject<Projects.Deopeia_Quote_Api>("deopeia-quote-api").WithReference(dbQuote);

builder.AddProject<Projects.Deopeia_Quote_Worker>("deopeia-quote-worker");

builder.Build().Run();
