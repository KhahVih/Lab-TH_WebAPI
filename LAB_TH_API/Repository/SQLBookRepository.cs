using LAB_TH_API.Data;
using LAB_TH_API.Models;
using LAB_TH_API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace LAB_TH_API.Services
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly AppDbContext _dbcontext;

        public SQLBookRepository(AppDbContext db)
        {
            _dbcontext = db;
        }
        public List<BookDTO> GetAllBooks()
        {
             
            var bookDTO = _dbcontext.Books.Select(book => new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = book.DateAdded,
                PublishersName = book.publishers.Name,
                AuthorName = book.Book_Author.Select(b => b.authors.FullName).ToList()
            }).ToList();
            return bookDTO;
        }

        public BookDTO GetBookById(int id)
        {
            var book = _dbcontext.Books.Where(b => b.Id == id);
           
            var bookDTO = book.Select(book => new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = book.DateAdded,
                PublishersName = book.publishers.Name,
                AuthorName = book.Book_Author.Select(ba => ba.authors.FullName).ToList()
            }).FirstOrDefault();
            return bookDTO;
        }

        public AddBookDTO AddBook(AddBookDTO addbookDTO)
        {
            var book = new Books
            {
                Title = addbookDTO.Title,
                Description = addbookDTO.Description,
                IsRead = addbookDTO.IsRead,
                DateAdded = addbookDTO.DateAdded,
                Rate = addbookDTO.Rate,
                Genre = addbookDTO.Genre,
                CoverUrl = addbookDTO.CoverUrl,
                DateRead = addbookDTO.DateRead,
                PublishersId = addbookDTO.PublisherId
            };
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChangesAsync();
            foreach (var id in addbookDTO.AuthorIds)
            {
                var book_author = new Book_Author()
                {
                    BookId = book.Id,
                    AuthorId = id
                };
                _dbcontext.BooksAuthor.Add(book_author);
                _dbcontext.SaveChangesAsync();
            }
            return addbookDTO;
        }

        public AddBookDTO? UpdateBookById(int id, AddBookDTO bookDTO)
        {
            var book = _dbcontext.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Title = bookDTO.Title;
                book.Description = bookDTO.Description;
                book.IsRead = bookDTO.IsRead;
                book.DateAdded = bookDTO.DateAdded;
                book.Rate = bookDTO.Rate;
                book.Genre = bookDTO.Genre;
                book.CoverUrl = bookDTO.CoverUrl;
                book.DateRead = bookDTO.DateRead;
                book.PublishersId = bookDTO.PublisherId;
                _dbcontext.SaveChanges();
            }
           
           
            var author = _dbcontext.BooksAuthor.Where(a => a.BookId == id).ToList();
            if (author != null)
            {
                _dbcontext.BooksAuthor.RemoveRange(author);
                _dbcontext.SaveChanges();
            }
            foreach (var authorid in bookDTO.AuthorIds)
            {
                var book_author = new Book_Author()
                {
                    BookId = id,
                    AuthorId = authorid,
                };
            }
            return bookDTO;
        }

        public Books? DeleteBookById(int id)
        {
            var book = _dbcontext.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _dbcontext.Books.Remove(book);
                _dbcontext.SaveChangesAsync();
            }
            return  book;
        }
    }
}
