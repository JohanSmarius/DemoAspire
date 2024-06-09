using EventsApi.Models;

namespace EventsApi
{
    public class EventsService
    {
        private readonly ILogger<EventsService> logger;

        public EventsService(ILogger<EventsService> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<Event> GetEvents()
        {
            var events = new List<Event>()
            { 
                // Generate 5 fake data entries
                new Event { Id = 1, Name = "Event 1 From Api", StartTime = DateTime.Now, EndTime = DateTime.Now, Location = "Location 1", Description = "Description 1" },
                new Event { Id = 2, Name = "Event 2 From API", StartTime = DateTime.Now, EndTime = DateTime.Now, Location = "Location 2", Description = "Description 2" },
                new Event { Id = 3, Name = "Event 3 From API", StartTime = DateTime.Now, EndTime = DateTime.Now, Location = "Location 3", Description = "Description 3" },
            };

            logger.LogInformation($"About to return {events.Count} events");
            return events;
        }
    }
}
