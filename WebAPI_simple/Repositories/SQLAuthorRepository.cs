using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;
using WebAPI_simple.Models.DTO;

namespace WebAPI_simple.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public SQLAuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<AuthorDTO> GetAllAuthors()
        {
            return _context.Authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                FullName = a.FullName
            }).ToList();
        }

        public AuthorNoIdDTO? GetAuthorById(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            return new AuthorNoIdDTO { FullName = author.FullName };
        }

        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var author = new Author { FullName = addAuthorRequestDTO.FullName };
            _context.Authors.Add(author);
            _context.SaveChanges();
            return addAuthorRequestDTO;
        }

        public AuthorNoIdDTO? UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            author.FullName = authorNoIdDTO.FullName;
            _context.SaveChanges();
            return authorNoIdDTO;
        }

        public Author? DeleteAuthorById(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return author;
        }
    }
}
