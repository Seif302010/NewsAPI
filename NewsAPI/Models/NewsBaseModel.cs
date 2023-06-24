using AutoMapper.Configuration.Annotations;
using NewsAPI.Models.CustomValidators;

namespace NewsAPI.Models
{
    public class NewsBaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[]? image { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime publication_date { get; set; }
        public int AuthorId { get; set; }
    }
}
