var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var eventApi = builder.AddProject<Projects.EventsApi>("eventsapi").WithReference(redis);
var volunteerApi = builder.AddProject<Projects.VolunteerApi>("volunteerapi").WithReplicas(2);

builder.AddProject<Projects.EventApp>("eventapp")
    .WithReference(eventApi)
    .WithReference(volunteerApi)
    .WithReference(redis);

builder.Build().Run();
