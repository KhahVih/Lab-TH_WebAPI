using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LAB_TH_API.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateRead { get; set; }
        public int Rate { get; set; }
        public int Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        public int  PublishersId { get; set; }
        public Publishers publishers { get; set; }
        public List<Book_Author> Book_Author { get; set; }
    }
}
