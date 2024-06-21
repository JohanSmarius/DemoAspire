var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var dbPassword = builder.AddParameter("dbpassword", secret: true);

var dbServer = builder.AddPostgres("dbserver", password: dbPassword)
    .WithDataVolume()
    .WithPgAdmin();

var db = dbServer.AddDatabase("db");

var eventApi = builder.AddProject<Projects.EventsApi>("eventsapi");

var volunteerApi = builder.AddProject<Projects.VolunteerApi>("volunteerapi")
    .WithReference(redis)
    .WithReplicas(2);

builder.AddProject<Projects.EventApp>("eventapp")
    .WithReference(eventApi)
    .WithReference(volunteerApi)
    .WithReference(redis)
    .WithReference(db);


builder.AddProject<Projects.DemoAspire_MigrationWorker>("demoaspire-migrationworker")
    .WithReference(db);


builder.Build().Run();
