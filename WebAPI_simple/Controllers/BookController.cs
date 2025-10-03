using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.CustomActionFilter;
using WebAPI_simple.Models.DTO;
using WebAPI_simple.Repositories;

namespace WebAPI_simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("get-all-books")]
                public IActionResult GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 100
)
        {
            var allBooks = _bookRepository.GetAllBooks(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(allBooks);
        }

        [HttpPost("add-book")]
        [ValidateModel] 
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            var book = _bookRepository.AddBook(addBookRequestDTO);
            return Ok(book);
        }

        [HttpPut("update-book-by-id/{id}")]
        [ValidateModel] 
        public IActionResult UpdateBook(int id, [FromBody] AddBookRequestDTO updateBookDTO)
        {
            var book = _bookRepository.UpdateBookById(id, updateBookDTO);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookRepository.DeleteBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
    }
}
