namespace NewsAPI.Services
{
    public interface INewsService
    {
        Task<IEnumerable<News>> Get(string? title,string?author,DateTime? dateTime);
        Task<News> GetById(int id);
        Task<News> Post(News news);
        News Put(News news);
        News Delete(News news);
        IEnumerable<NewsDetailsDto> Map(IEnumerable<News> news);
    }
}
