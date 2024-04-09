namespace MoviesAPI.DTOs
{
    public class CreateActorDTO
    {
        public string Name { get; set; } = String.Empty;
        public DateTime Birthdate { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
