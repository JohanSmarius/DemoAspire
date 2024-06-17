var builder = DistributedApplication.CreateBuilder(args);

var dbPassword = builder.AddParameter("dbpassword", secret: true);

var dbServer = builder.AddPostgres("dbserver", password: dbPassword).WithPgAdmin().WithExternalHttpEndpoints();
var db = dbServer.AddDatabase("db");

var redis = builder.AddRedis("redis");

var eventApi = builder.AddProject<Projects.EventsApi>("eventsapi").WithReference(redis).WithExternalHttpEndpoints();
var volunteerApi = builder.AddProject<Projects.VolunteerApi>("volunteerapi").WithReplicas(2).WithExternalHttpEndpoints();

builder.AddProject<Projects.EventApp>("eventapp")
    .WithReference(eventApi)
    .WithReference(volunteerApi)
    .WithReference(redis)
    .WithReference(db)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.Postgress_Migrations>("postgress-migrations")
    .WithReference(db);

builder.Build().Run();
