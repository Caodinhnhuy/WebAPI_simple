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

        public List<Publisher> GetAllPublishers()
        {
            return _context.Publishers.ToList();
        }

        public Publisher? GetPublisherById(int id)
        {
            return _context.Publishers.FirstOrDefault(p => p.Id == id);
        }

        public Publisher AddPublisher(AddPublisherRequestDTO publisherDTO)
        {
          
            var exists = _context.Publishers.Any(p => p.Name == publisherDTO.Name);
            if (exists)
            {
                throw new Exception("Publisher name already exists");
            }

            var publisher = new Publisher
            {
                Name = publisherDTO.Name
            };

            _context.Publishers.Add(publisher);
            _context.SaveChanges();

            return publisher;
        }


        public Publisher? UpdatePublisher(int id, AddPublisherRequestDTO publisherDTO)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            publisher.Name = publisherDTO.Name;
            _context.SaveChanges();

            return publisher;
        }

        public Publisher? DeletePublisher(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            _context.Publishers.Remove(publisher);
            _context.SaveChanges();

            return publisher;
        }
    }
}
