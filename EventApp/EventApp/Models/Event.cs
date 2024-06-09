namespace EventApp.Models;

public class Event
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Customer { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public List<Volunteer> Volunteers { get; set; } = new();
}
