using EventApp.Models;

namespace EventApp.Infrastructure
{
    public class VolunteerApiClient(HttpClient httpClient)
    {
        public async Task<Volunteer[]> GetVolunteersAsync(int maxItems = 10, CancellationToken cancellationToken = default)
        {
            List<Volunteer>? volunteers = null;

            await foreach (var volunteer in httpClient.GetFromJsonAsAsyncEnumerable<Volunteer>("/volunteer", cancellationToken))
            {
                if (volunteers?.Count >= maxItems)
                {
                    break;
                }
                if (volunteer is not null)
                {
                    volunteers ??= [];
                    volunteers.Add(volunteer);
                }
            }

            return volunteers?.ToArray() ?? [];
        }

    }
}
