using Microsoft.AspNetCore.Authorization;
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
        private readonly AppDbContext _context;

        private readonly ILogger<BooksController> _logger;

        public BooksController(AppDbContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

      
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            _logger.LogInformation(" API GetAllBooks() được gọi lúc {time}", DateTime.Now);

            var books = _context.Books.ToList();
            _logger.LogInformation("Tổng số sách: {count}", books.Count);

            return Ok(books);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            _logger.LogWarning("⚠ Admin đang xóa sách có Id = {id}", id);

            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                _logger.LogError("Không tìm thấy sách có Id = {id}", id);
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            _logger.LogInformation("Đã xóa sách có Id = {id}", id);
            return Ok("Book deleted successfully!");
        }
    }
}
