using DemoAspire.MigrationWorker;
using DemoAspire.Postgress;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("db"))
);

builder.Services.AddOpenTelemetry()
        .WithTracing(tracing => tracing.AddSource(Worker.ActivityName));

var host = builder.Build();
host.Run();
