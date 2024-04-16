namespace LAB_TH_API.Models
{
    public class Book_Author
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Books books { get; set; }
        public int AuthorId { get; set; }
        public Authors authors { get; set; }

    }
}
