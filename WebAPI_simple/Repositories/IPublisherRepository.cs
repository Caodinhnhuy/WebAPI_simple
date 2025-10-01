using WebAPI_simple.Models.Domain;
using WebAPI_simple.Models.DTO;

namespace WebAPI_simple.Repositories
{
    public interface IPublisherRepository
    {
        List<Publisher> GetAllPublishers();
        Publisher? GetPublisherById(int id);
        Publisher AddPublisher(AddPublisherRequestDTO publisherDTO);
        Publisher? UpdatePublisher(int id, AddPublisherRequestDTO publisherDTO);
        Publisher? DeletePublisher(int id);
    }
}
