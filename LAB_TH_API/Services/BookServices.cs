using LAB_TH_API.Data;
using LAB_TH_API.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB_TH_API.Services
{
    public class BookServices : IBookServices
    {
        private readonly AppDbContext _db;

        public BookServices(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<Books>> GetBooksAsync()
        {
            var book = await _db.Books.ToListAsync();
            return book;
        }

        public async Task<Books> GetBookAsync(Guid id)
        {
            try
            {
                return await _db.Books.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Books> AddBookAsync(Books book)
        {
            try
            {
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();
                return await _db.Books.FindAsync(book.Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Books> UpdateBookAsync(Books book)
        {
            try
            {
                _db.Entry(book).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return book;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteBookAsync(Books book)
        {
            try
            {
                var dbBook = await _db.Books.FindAsync(book.Id);

                if (dbBook == null)
                {
                    return (false, "Book could not be found.");
                }

                _db.Books.Remove(book);
                await _db.SaveChangesAsync();

                return (true, "Book got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
    }
}
