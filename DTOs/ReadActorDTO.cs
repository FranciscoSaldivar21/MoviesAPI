namespace MoviesAPI.DTOs
{
    public class ReadActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public DateTime Birthdate { get; set; }
        public string Picture { get; set; } = String.Empty;
    }
}
