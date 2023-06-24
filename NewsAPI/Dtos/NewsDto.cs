using NewsAPI.Models.CustomValidators;

namespace NewsAPI.Dtos
{
    public class NewsDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public IFormFile? image { get; set; }

        [Required,FromNowToAfterOneWeek]
        public DateTime publication_date { get; set; }

        public int AuthorId { get; set; }
    }
}
