using VolunteerApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
.WithOpenApi();

app.Run();


