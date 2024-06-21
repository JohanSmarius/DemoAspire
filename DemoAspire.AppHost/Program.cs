var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.EventApp>("eventapp");

builder.Build().Run();
