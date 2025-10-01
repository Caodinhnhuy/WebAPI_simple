using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.CustomActionFilter;
using WebAPI_simple.Models.Domain;
using WebAPI_simple.Models.DTO;
using WebAPI_simple.Repositories;

namespace WebAPI_simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public IActionResult GetAllPublishers()
        {
            var publishers = _publisherRepository.GetAllPublishers();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = _publisherRepository.GetPublisherById(id);
            if (publisher == null) return NotFound();
            return Ok(publisher);
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult AddPublisher([FromBody] AddPublisherRequestDTO addPublisherRequestDTO)
        {
            try
            {
                var addedPublisher = _publisherRepository.AddPublisher(addPublisherRequestDTO);
                return Ok(addedPublisher);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        [ValidateModel]
        public IActionResult UpdatePublisher(int id, [FromBody] AddPublisherRequestDTO updatePublisherDTO)
        {
            var updatedPublisher = _publisherRepository.UpdatePublisher(id, updatePublisherDTO);
            if (updatedPublisher == null) return NotFound();
            return Ok(updatedPublisher);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePublisher(int id)
        {
            var deletedPublisher = _publisherRepository.DeletePublisher(id);
            if (deletedPublisher == null) return NotFound();
            return Ok(deletedPublisher);
        }
    }
}