using VolunteerApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddRedisDistributedCache("cache");

builder.Services.AddOutputCache(options => 
{
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(60)));
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOutputCache();

app.MapGet("/volunteer", () =>
{
    // Generate a list of 5 fake volunteers including fake certifications
    var volunteers = new List<Volunteer>();

    var randomGenerator = new Random();
    for (var i = 1; i <= 5; i++)
    {
        var volunteer = new Volunteer
        {
            Id = i,
            RegistrationNumber = randomGenerator.Next(10000),
            Name = $"Volunteer {i}",
            Email = $"x{i}@mail.com",
            Certifications = new List<Certification>()
            {
                new Certification
                {
                    Id = 1,
                    Name = "First Aid",
                    ValidUntil = DateTime.Now.AddYears(1)
                },
            }
        };
        volunteers.Add(volunteer);
    }
    return volunteers;
})
.WithName("GetVolunteers")
.WithOpenApi().CacheOutput();


app.Run();


