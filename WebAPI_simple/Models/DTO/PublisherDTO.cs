﻿namespace WebAPI_simple.Models.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PublisherNoIdDTO
    {
        public string Name { get; set; } = string.Empty;
    }

    // extra model để lấy cả sách và tác giả theo Publisher
    public class PublisherWithBooksAndAuthorsDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<BookAuthorDTO> BookAuthors { get; set; } = new();
    }

    public class BookAuthorDTO
    {
        public string BookName { get; set; } = string.Empty;
        public List<string> BookAuthors { get; set; } = new();
    }
}
