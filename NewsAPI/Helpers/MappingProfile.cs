namespace NewsAPI.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsDetailsDto>();
            CreateMap<NewsDto, News>()
                .ForMember(src => src.image, opt => opt.Ignore());
        }
    }
}
