using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.Models.DTO;
using WebAPI_simple.Repositories;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok(_authorRepository.GetAllAuthors());
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public IActionResult AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var author = _authorRepository.AddAuthor(addAuthorRequestDTO);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.FullName }, author);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var updated = _authorRepository.UpdateAuthorById(id, authorNoIdDTO);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            var deleted = _authorRepository.DeleteAuthorById(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
