using EventApp.Models;

namespace EventApp.Infrastructure
{
    public class EventsApiClient(HttpClient httpClient)
    {
        public async Task<Event[]> GetEventsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
        {
            List<Event>? events = null;

            await foreach (var e in httpClient.GetFromJsonAsAsyncEnumerable<Event>("/event", cancellationToken))
            {
                if (events?.Count >= maxItems)
                {
                    break;
                }
                if (e is not null)
                {
                    events ??= [];
                    events.Add(e);
                }
            }

            return events?.ToArray() ?? [];
        }

        public async Task<Event> GetEventByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var apiCall = $"/event/{id}";
            var result = await httpClient.GetFromJsonAsync<Event>(apiCall, cancellationToken);

            return result ?? new Event();
        }
    }
}
