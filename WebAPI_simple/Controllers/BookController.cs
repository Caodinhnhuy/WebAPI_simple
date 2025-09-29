using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;
using WebAPI_simple.Models.DTO;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BooksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/books/get-all-books
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooks = _dbContext.Books.ToList();

            var allBooksDTO = allBooks.Select(book => new
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = book.DateAdded,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(a => a.Author.FullName).ToList()
            });

            return Ok(allBooksDTO);
        }

        // GET: api/books/get-book-by-id/{id}
        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDTO = new
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = book.DateAdded,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(a => a.Author.FullName).ToList()
            };

            return Ok(bookDTO);
        }

        // POST: api/books/add-book
        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] AddBookRequestDTO bookDTO)
        {
            var bookDomain = new Book
            {
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                IsRead = bookDTO.IsRead,
                DateRead = bookDTO.DateRead,
                Rate = bookDTO.Rate,
                Genre = bookDTO.Genre,
                CoverUrl = bookDTO.CoverUrl,
                DateAdded = bookDTO.DateAdded,
                PublisherID = bookDTO.PublisherID
            };

            _dbContext.Books.Add(bookDomain);
            _dbContext.SaveChanges();

            foreach (var authorId in bookDTO.AuthorIds)
            {
                var bookAuthor = new Book_Author
                {
                    BookId = bookDomain.Id,
                    AuthorId = authorId
                };
                _dbContext.Books_Authors.Add(bookAuthor);
                _dbContext.SaveChanges();
            }

            return Ok(bookDTO);
        }

        // PUT: api/books/update-book-by-id/{id}
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(b => b.Id == id);

            if (bookDomain == null)
            {
                return NotFound();
            }

            bookDomain.Title = bookDTO.Title;
            bookDomain.Description = bookDTO.Description;
            bookDomain.IsRead = bookDTO.IsRead;
            bookDomain.DateRead = bookDTO.DateRead;
            bookDomain.Rate = bookDTO.Rate;
            bookDomain.Genre = bookDTO.Genre;
            bookDomain.CoverUrl = bookDTO.CoverUrl;
            bookDomain.DateAdded = bookDTO.DateAdded;
            bookDomain.PublisherID = bookDTO.PublisherID;

            _dbContext.SaveChanges();

            var existingAuthors = _dbContext.Books_Authors.Where(a => a.BookId == id).ToList();
            if (existingAuthors.Any())
            {
                _dbContext.Books_Authors.RemoveRange(existingAuthors);
                _dbContext.SaveChanges();
            }

            foreach (var authorId in bookDTO.AuthorIds)
            {
                var bookAuthor = new Book_Author
                {
                    BookId = id,
                    AuthorId = authorId
                };
                _dbContext.Books_Authors.Add(bookAuthor);
                _dbContext.SaveChanges();
            }

            return Ok(bookDTO);
        }

        // DELETE: api/books/delete-book-by-id/{id}
        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(b => b.Id == id);

            if (bookDomain != null)
            {
                _dbContext.Books.Remove(bookDomain);
                _dbContext.SaveChanges();
            }

            return Ok();
        }
    }
}
