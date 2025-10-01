using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;
using WebAPI_simple.Models.DTO;

namespace WebAPI_simple.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;

        public SQLPublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            return _context.Publishers.Select(p => new PublisherDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public PublisherNoIdDTO? GetPublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            return new PublisherNoIdDTO { Name = publisher.Name };
        }

        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var publisher = new Publisher { Name = addPublisherRequestDTO.Name };
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return addPublisherRequestDTO;
        }

        public PublisherNoIdDTO? UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            publisher.Name = publisherNoIdDTO.Name;
            _context.SaveChanges();
            return publisherNoIdDTO;
        }

        public Publisher? DeletePublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
            return publisher;
        }
    }
}
