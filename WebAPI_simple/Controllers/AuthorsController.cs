using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.CustomActionFilter;
using WebAPI_simple.Models.DTO;
using WebAPI_simple.Repositories;

namespace WebAPI_simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var authors = _authorRepository.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult AddAuthor([FromBody] AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var author = _authorRepository.AddAuthor(addAuthorRequestDTO);
            return Ok(author);
        }

        [HttpPut("{id}")]
        [ValidateModel] 
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorNoIdDTO authorNoIdDTO)
        {
            var author = _authorRepository.UpdateAuthorById(id, authorNoIdDTO);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _authorRepository.DeleteAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }
    }
}
