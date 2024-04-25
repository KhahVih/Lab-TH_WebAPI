using LAB_TH_API.Models;

namespace LAB_TH_API.Services
{
    public interface IBookServices
    {
        Task<List<Books>> GetBooksAsync(); // GET All Books
        Task<Books> GetBookAsync(Guid id); // Get Single Book
        Task<Books> AddBookAsync(Books book); // POST New Book
        Task<Books> UpdateBookAsync(Books book); // PUT Book
        Task<(bool, string)> DeleteBookAsync(Books book); // DELETE Book
    }
}
