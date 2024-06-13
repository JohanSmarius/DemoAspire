var builder = DistributedApplication.CreateBuilder(args);

var dbPassword = builder.AddParameter("dbPassword", secret: true);

var dbServer = builder.AddPostgres("dbServer", password: dbPassword).WithDataVolume().WithPgAdmin();
var db = dbServer.AddDatabase("db");

var redis = builder.AddRedis("redis");

var eventApi = builder.AddProject<Projects.EventsApi>("eventsapi").WithReference(redis);
var volunteerApi = builder.AddProject<Projects.VolunteerApi>("volunteerapi").WithReplicas(2);

builder.AddProject<Projects.EventApp>("eventapp")
    .WithReference(eventApi)
    .WithReference(volunteerApi)
    .WithReference(redis)
    .WithReference(db);

builder.AddProject<Projects.Postgress_Migrations>("postgress-migrations")
    .WithReference(db);

builder.Build().Run();
