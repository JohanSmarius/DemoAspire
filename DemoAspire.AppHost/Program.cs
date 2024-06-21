var builder = DistributedApplication.CreateBuilder(args);


var eventApi = builder.AddProject<Projects.EventsApi>("eventsapi");

builder.AddProject<Projects.EventApp>("eventapp").WithReference(eventApi);



builder.Build().Run();
