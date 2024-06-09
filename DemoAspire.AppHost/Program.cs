var builder = DistributedApplication.CreateBuilder(args);

var eventApi = builder.AddProject<Projects.EventsApi>("eventsapi");
var volunteerApi = builder.AddProject<Projects.VolunteerApi>("volunteerapi").WithReplicas(2);

builder.AddProject<Projects.EventApp>("eventapp").WithReference(eventApi).WithReference(volunteerApi);  

builder.Build().Run();
