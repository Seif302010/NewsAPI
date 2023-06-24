using NewsAPI.Models;

namespace NewsAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NewsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<News>> Get(string? title, string? author, DateTime? dateTime)
        {
            return await _context.News.Include(m => m.Author)
                .Where(n => n.publication_date <= (dateTime == null ? DateTime.MaxValue : dateTime))
                .Where(n => n.Title.Contains(title == null ? "" : title))
                .Where(a => a.Author.Name.Contains(author == null ? "" : author))
                .ToListAsync();
        }

        public async Task<News> GetById(int id)
        {
            return await _context.News.Include(m => m.Author).SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<News> Post(News news)
        {
             await _context.News.AddAsync(news);
            _context.SaveChanges();
            return news;
        }

        public News Put(News news)
        {
            _context.News.Update(news);
            _context.SaveChanges();
            return news;
        }
        public News Delete(News news)
        {
            _context.News.Remove(news);
            _context.SaveChanges();
            return news;
        }

        public IEnumerable<NewsDetailsDto> Map(IEnumerable<News> news)
        {
           return _mapper.Map<IEnumerable<NewsDetailsDto>>(news);
        }
    }
}
