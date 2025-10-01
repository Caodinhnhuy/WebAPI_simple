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
        public IActionResult GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost("add-book")]
        [ValidateModel]  // ✅ Validate tự động
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            var book = _bookRepository.AddBook(addBookRequestDTO);
            return Ok(book);
        }

        [HttpPut("update-book-by-id/{id}")]
        [ValidateModel]  // ✅ Validate tự động
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
