namespace MoviesAPI.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public DateTime Birthdate { get; set; }
        public string Picture { get; set; } = String.Empty;
    }
}
