namespace VolunteerApi.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        public int RegistrationNumber { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<Certification> Certifications { get; set; } = new();
    }
}
