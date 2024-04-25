namespace LAB_TH_API.Models
{
    public class Authors
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public List<Book_Author>? Book_Author { get; set; }
    }
}
