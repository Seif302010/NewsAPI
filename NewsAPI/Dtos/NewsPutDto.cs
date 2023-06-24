using NewsAPI.Models.CustomValidators;

namespace NewsAPI.Dtos
{
    public class NewsPutDto
    {
        [MaxLength(100)]
        public string? Title { get; set; }
        public string? Content { get; set; }

        [FromNowToAfterOneWeek]
        public DateTime? publication_date { get; set; }
        public IFormFile? image { get; set; }
        public int AuthorId { get; set; }
    }
}
