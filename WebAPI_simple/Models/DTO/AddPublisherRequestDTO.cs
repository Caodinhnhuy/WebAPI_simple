using System.ComponentModel.DataAnnotations;

namespace WebAPI_simple.Models.DTO
{
    public class AddPublisherRequestDTO
    {
        [Required(ErrorMessage = "Publisher Name is required")]
        [MinLength(3, ErrorMessage = "Publisher Name must be at least 3 characters")]
        public string Name { get; set; } = string.Empty;
    }
}
