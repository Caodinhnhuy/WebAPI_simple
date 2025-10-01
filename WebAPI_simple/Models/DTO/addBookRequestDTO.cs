public class AddBookRequestDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime? DateRead { get; set; }
    public int Rate { get; set; }
    public string Genre { get; set; } = string.Empty;
    public string CoverUrl { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.Now;
    public int PublisherId { get; set; }
    public int PublisherID { get; internal set; }
    public List<int> AuthorIds { get; set; } = new List<int>();
}
