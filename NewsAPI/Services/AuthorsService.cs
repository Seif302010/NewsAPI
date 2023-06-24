using Humanizer.Localisation;
using System.Xml.Linq;

namespace NewsAPI.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly ApplicationDbContext _context;

        public AuthorsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> Get(string? Name)
        {
            return await _context.Authors.OrderBy(a => a.Name).Where(a => a.Name.Contains(Name == null ? "" : Name)).ToListAsync();
        }

        public async Task<Author> GetById(int id)
        {
            return await _context.Authors.SingleOrDefaultAsync( a => a.Id == id);
        }

        public async Task<Author> Post(Author author)
        {
            if (await _context.Authors.SingleOrDefaultAsync(a => a.Name == author.Name) == null)
            {
                await _context.Authors.AddAsync(author);
                _context.SaveChanges();
            }
            else
                author.Id = -1;
            return author;
        }

        public Author Put(Author author)
        {
            if (_context.Authors.SingleOrDefault(a => a.Name == author.Name) == null)
            {
                _context.Authors.Update(author);
                _context.SaveChanges();
            }
            else
                author.Id = -1;
            return author;
        }
        public Author Delete(Author author)
        {
            _context.Remove(author);
            _context.SaveChanges();
            return author;
        }

    }
}
