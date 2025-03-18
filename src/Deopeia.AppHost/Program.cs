using Deopeia.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("postgresql-password", secret: true);
var postgres = builder.AddPostgres("postgres", password: password, port: 59999).WithDataVolume();
var dbIdentity = postgres.AddDatabase("identity");
var dbQuote = postgres.AddDatabase("quote");
var dbTrading = postgres.AddDatabase("trading");

var identityApi = builder
    .AddProject<Projects.Deopeia_Identity_Api>("deopeia-identity-api")
    .WithJwt()
    .WithMinIO()
    .WithKafka()
    .WithReferenceAndWaitFor(dbIdentity)
    .WithProxyEndpoint();

var notificationHub = builder
    .AddProject<Projects.Deopeia_Notification_Hub>("deopeia-notification-hub")
    .WithKafka();

var quoteApi = builder
    .AddProject<Projects.Deopeia_Quote_Api>("deopeia-quote-api")
    .WithMinIO()
    .WithKafka()
    .WithReferenceAndWaitFor(dbQuote)
    .WithProxyEndpoint();

builder
    .AddProject<Projects.Deopeia_Quote_Worker>("deopeia-quote-worker")
    .WithMinIO()
    .WithKafka()
    .WithReferenceAndWaitFor(dbQuote);

var tradingApi = builder
    .AddProject<Projects.Deopeia_Trading_Api>("deopeia-trading-api")
    .WithMinIO()
    .WithKafka()
    .WithReferenceAndWaitFor(dbTrading)
    .WithProxyEndpoint();

builder
    .AddProject<Projects.Deopeia_Trading_Worker>("deopeia-trading-worker")
    .WithMinIO()
    .WithKafka()
    .WithReferenceAndWaitFor(dbTrading);

builder
    .AddProject<Projects.Deopeia_Finance_Bff>("deopeia-finance-bff")
    .WithJwt()
    .WithReference(identityApi)
    .WithReference(notificationHub)
    .WithReference(quoteApi)
    .WithReference(tradingApi);

builder.Build().Run();
