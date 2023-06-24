namespace NewsAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Author
    {
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string Name { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}
