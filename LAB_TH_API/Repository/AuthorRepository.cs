using LAB_TH_API.Data;
using LAB_TH_API.Models;
using LAB_TH_API.Models.DTO;

namespace LAB_TH_API.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbcontext;
        public AuthorRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public List<AuthorDTO> GetAllAuthor()
        {
            var authorDTO = _dbcontext.Authors.Select(author => new AuthorDTO
            {
                Id = author.Id,
                FullName = author.FullName,
                NameBook = author.Book_Author.Select(ba => ba.books.Title).ToList(),
            }).ToList();
            return authorDTO;
        }
        public AuthorDTO GetAuthorById(int id)
        {
            var author = _dbcontext.Authors.Where(b => b.Id == id);
           
            var authorDTO = author.Select(author => new AuthorDTO()
            {
                Id = author.Id,
                FullName = author.FullName,
                NameBook = author.Book_Author.Select(ba => ba.books.Title).ToList(),
            }).FirstOrDefault();
            return authorDTO;
        }
        public AddAuthorDTO AddAuthor(AddAuthorDTO addauthorDTO)
        {
            var author = new Authors
            {
                FullName = addauthorDTO.FullName,
            };
            _dbcontext.Authors.Add(author);
            _dbcontext.SaveChangesAsync();
            foreach (var id in addauthorDTO.BookIds)
            {
                var book_author = new Book_Author()
                {
                   AuthorId = author.Id,
                   BookId = id
                };
                _dbcontext.BooksAuthor.Add(book_author);
                _dbcontext.SaveChangesAsync();
            }
            return addauthorDTO;
        }
        public AddAuthorDTO? UpdateAuthorById(int id, AddAuthorDTO authorDTO)
        {
            var author = _dbcontext.Authors.FirstOrDefault(a => a.Id == id);
           if (author != null)
            {
                author.FullName = authorDTO.FullName;
                _dbcontext.SaveChanges();
            }
            var book = _dbcontext.BooksAuthor.Where(a => a.AuthorId == id).ToList();
            if (author != null)
            {
                _dbcontext.BooksAuthor.RemoveRange(book);
                _dbcontext.SaveChanges();
            }
            foreach (var authorid in authorDTO.BookIds)
            {
                var book_author = new Book_Author()
                {
                    BookId = id,
                    AuthorId = authorid,
                };
            }
            return authorDTO;
        }
        public Authors? DeleteAuthorById(int id)
        {
            var author = _dbcontext.Authors.FirstOrDefault(a => a.Id == id);
            if (author != null)
            {
                _dbcontext.Authors.Remove(author);
                _dbcontext.SaveChanges();
            }
            return author;
        }
    }
}
