using AutoMapper.Configuration.Annotations;
using Humanizer.Localisation;
using NewsAPI.Models.CustomValidators;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace NewsAPI.Models
{
    public class News: NewsBaseModel
    {
        [Required, MaxLength(100)]
        public  string Title { get; set; }
        [Required]
        public  string Content { get; set; }

        [Required, FromNowToAfterOneWeek]
        public  DateTime publication_date { get; set; }

        public Author Author { get; set; }
    }
}
