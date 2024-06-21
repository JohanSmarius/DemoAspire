using EventsApi.Models;
using EventsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EventsService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/event", (EventsService service) =>
{
    return service.GetEvents();
})
.WithName("GetEvents")
.WithOpenApi();

app.MapGet("/event/{id}", (int id) =>
{
    var events = new List<Event>()
    {
        // Generate 5 fake data entries
        new Event { Id = 1, Name = "Event 1 From Api", StartTime = DateTime.Now, EndTime = DateTime.Now, Location = "Location 1", Description = "Description 1" },
        new Event { Id = 2, Name = "Event 2 From API", StartTime = DateTime.Now, EndTime = DateTime.Now, Location = "Location 2", Description = "Description 2" },
        new Event { Id = 3, Name = "Event 3 From API", StartTime = DateTime.Now, EndTime = DateTime.Now, Location = "Location 3", Description = "Description 3" },
    };

    var foundEvent = events.SingleOrDefault(e => e.Id == id);

    return foundEvent ?? new Event();
})
.WithName("GetEventById")
.WithOpenApi();

app.Run();

