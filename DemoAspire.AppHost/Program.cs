var builder = DistributedApplication.CreateBuilder(args);

var dbServer = builder.AddPostgres("dbServer").WithPgAdmin();
var db = dbServer.AddDatabase("db");

var redis = builder.AddRedis("redis");

var eventApi = builder.AddProject<Projects.EventsApi>("eventsapi").WithReference(redis);
var volunteerApi = builder.AddProject<Projects.VolunteerApi>("volunteerapi").WithReplicas(2);

builder.AddProject<Projects.EventApp>("eventapp")
    .WithReference(eventApi)
    .WithReference(volunteerApi)
    .WithReference(redis)
    .WithReference(db);

builder.Build().Run();
