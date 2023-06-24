using Humanizer.Localisation;
using System.Xml.Linq;

namespace NewsAPI.Services
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Author>> Get(string? Name);
        Task<Author> GetById(int id);
        Task<Author> Post(Author author);
        Author Put(Author author);
        Author Delete(Author author);
    }
}
