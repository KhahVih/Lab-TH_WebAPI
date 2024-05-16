using LAB_TH_API.Data;
using LAB_TH_API.Models;
using LAB_TH_API.Models.DTO;
using LAB_TH_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace LAB_TH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        protected readonly AppDbContext _dbcontext;
        private readonly IBookRepository _bookrepository;
        private readonly ILogger<BookController> _logger;
        public BookController(AppDbContext dbcontext, IBookRepository bookServices, ILogger<BookController> logger )
        {
            _dbcontext = dbcontext;
            _bookrepository = bookServices;
            _logger = logger;
           
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string? sortby, [FromQuery] bool isacsending,
        [FromQuery] string? filteron, [FromQuery] string? filterquery,
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            //var book = _dbcontext.Books.Include(b => b.publishers).Include(b => b.Book_Author).ThenInclude(a => a.authors).ToList();
            //if (book == null || !book.Any())
            //{
            //    return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
            //}
            //var bookDTO = book.Select(book => new BookDTO
            //{
            //    Id = book.Id,
            //    Title = book.Title,
            //    Description = book.Description,
            //    IsRead = book.IsRead,
            //    DateRead = book.DateRead,
            //    Rate = book.Rate,
            //    Genre = book.Genre,
            //    CoverUrl = book.CoverUrl,
            //    DateAdded = book.DateAdded,
            //    PublishersName = book.publishers?.Name,
            //    AuthorName = book.Book_Author.Select(b => b.authors.FullName).ToList(),
            //}).ToList();
            try
            {
                _logger.LogInformation("Get All Book Action Method Was Invoked");
                var allbook =  _bookrepository.GetAllBooks();
               
                
                    // filterring
                    if (string.IsNullOrWhiteSpace(filteron) == false && string.IsNullOrWhiteSpace(filterquery) == false)
                    {
                        if (!string.IsNullOrWhiteSpace(filteron) && filteron.Equals("name", StringComparison.OrdinalIgnoreCase) &&
                            !string.IsNullOrWhiteSpace(filterquery))
                        {
                            allbook = allbook.Where(x => x.Title.Contains(filterquery, StringComparison.OrdinalIgnoreCase)).ToList();
                        }
                    }
                    // sortby
                    if (!string.IsNullOrEmpty(sortby))
                    {
                        switch (sortby.ToLower())
                        {
                            case "name":
                                {
                                    allbook = isacsending ? allbook.OrderBy(s => s.Title).ToList() : allbook.OrderByDescending(s => s.Title).ToList();
                                    break;
                                }
                            case "id":
                                {
                                    allbook = isacsending ? allbook.OrderBy(s => s.Id).ToList() : allbook.OrderByDescending(s => s.Id).ToList();
                                    break;
                                }

                        }
                    }
                   
                
                _logger.LogInformation("Successfully fetched Book data.");
                return Ok(allbook);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching Book data: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching Book data");
            }
            
           
        }

        [HttpGet("Get-Id")]
        public async Task<IActionResult> GetById( int id)
        {
            //var book = _dbcontext.Books.Where(b => b.Id == id); 
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //var bookDTO = book.Select(book => new BookDTO()
            //{
            //    Id = book.Id,
            //    Title = book.Title,
            //    Description = book.Description,
            //    IsRead = book.IsRead,
            //    DateRead = book.DateRead,
            //    Rate = book.Rate,
            //    Genre = book.Genre,
            //    CoverUrl = book.CoverUrl,
            //    DateAdded = book.DateAdded,
            //    PublishersName = book.publishers.Name,
            //    AuthorName = book.Book_Author.Select(ba => ba.authors.FullName).ToList()
            //});
            var getbookid = _bookrepository.GetBookById(id);
            return Ok(getbookid);
        }
        [HttpPost("AddBook")]
        
        public async Task<IActionResult> AddBook([FromBody] AddBookDTO addbookDTO)
        {
            //var book = new Books
            //{
            //    Title = addbookDTO.Title,
            //    Description = addbookDTO.Description,
            //    IsRead = addbookDTO.IsRead,
            //    DateAdded = addbookDTO.DateAdded,
            //    Rate = addbookDTO.Rate,
            //    Genre = addbookDTO.Genre,
            //    CoverUrl = addbookDTO.CoverUrl,
            //    DateRead = addbookDTO.DateRead,
            //    PublishersId = addbookDTO.PublisherId
            //};
            //_dbcontext.Books.Add(book);
            //await _dbcontext.SaveChangesAsync();
            //foreach(var id in addbookDTO.AuthorIds)
            //{
            //    var book_author = new Book_Author()
            //    {
            //        BookId = book.Id,
            //        AuthorId = id
            //    };
            //}
            if (ModelState.IsValid)
            {
                var addbook = _bookrepository.AddBook(addbookDTO);
                return StatusCode(StatusCodes.Status200OK, addbook);
            }
            else return BadRequest(ModelState);
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] AddBookDTO bookDTO)
        {
            
            //var book = await _dbcontext.Books.FindAsync(id);
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //book.Title = bookDTO.Title;
            //book.Description = bookDTO.Description;
            //book.IsRead = bookDTO.IsRead;
            //book.DateAdded = bookDTO.DateAdded;
            //book.Rate = bookDTO.Rate;
            //book.Genre = bookDTO.Genre;
            //book.CoverUrl = bookDTO.CoverUrl;
            //book.DateRead = bookDTO.DateRead;
            //book.PublishersId = bookDTO.PublisherId;
            //_dbcontext.SaveChanges();
            //var author = _dbcontext.BooksAuthor.Where( a => a.BookId == id ).ToList();
            //if (author != null)
            //{
            //    _dbcontext.BooksAuthor.RemoveRange(author);
            //    _dbcontext.SaveChanges();
            //}
            //foreach(var authorid in bookDTO.AuthorIds)
            //{
            //    var book_author = new Book_Author()
            //    {
            //        BookId = id,
            //        AuthorId = authorid,
            //    };
            //}
            var updatebook = _bookrepository.UpdateBookById(id, bookDTO);
            return StatusCode(StatusCodes.Status200OK, updatebook);
        }
        private bool BookExists(int id)
        {
            return _dbcontext.Books.Any(e => e.Id == id);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook (int id)
        {
            //var book = await _dbcontext.Books.FindAsync(id);
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //_dbcontext.Books.Remove(book);
            //await _dbcontext.SaveChangesAsync();
            //return StatusCode(StatusCodes.Status200OK, book);
            var deletebook = _bookrepository.DeleteBookById(id);
            return Ok(deletebook);
        }
    }
}
