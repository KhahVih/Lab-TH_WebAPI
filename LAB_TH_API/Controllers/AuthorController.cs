using LAB_TH_API.Data;
using LAB_TH_API.Models;
using LAB_TH_API.Models.DTO;
using LAB_TH_API.Repository;
using LAB_TH_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_TH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        protected readonly AppDbContext _dbcontext;
        protected readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(AppDbContext dbcontext, IAuthorRepository authorRepository, ILogger<AuthorController> logger)
        {
            _dbcontext = dbcontext;
            _authorRepository = authorRepository;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAuthor([FromQuery] string? sortby, [FromQuery] bool isacsending,
        [FromQuery] string? filteron, [FromQuery] string? filterquery,
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            //var author = _dbcontext.Authors.Include( a => a.Book_Author).ThenInclude( ba => ba.books).ToList();
            //if (author == null || !author.Any())
            //{
            //    return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
            //}
            //var authorDTO = author.Select(author => new AuthorDTO
            //{
            //    Id = author.Id,
            //    FullName = author.FullName,
            //    NameBook = author.Book_Author.Select(ba => ba.books.Title).ToList(),
            //}).ToList();
            //return StatusCode(StatusCodes.Status200OK, authorDTO);
            try
            {
                var authorall = _authorRepository.GetAllAuthor();
                // filterring
                if (string.IsNullOrWhiteSpace(filteron) == false && string.IsNullOrWhiteSpace(filterquery) == false)
                {
                    if (!string.IsNullOrWhiteSpace(filteron) && filteron.Equals("name", StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(filterquery))
                    {
                        authorall = authorall.Where(x => x.FullName.Contains(filterquery, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                }
                // sortby
                if (!string.IsNullOrEmpty(sortby))
                {
                    switch (sortby.ToLower())
                    {
                        case "name":
                            {
                                authorall = isacsending ? authorall.OrderBy(s => s.FullName).ToList() : authorall.OrderByDescending(s => s.FullName).ToList();
                                break;
                            }
                        case "id":
                            {
                                authorall = isacsending ? authorall.OrderBy(s => s.Id).ToList() : authorall.OrderByDescending(s => s.Id).ToList();
                                break;
                            }

                    }
                }
                return Ok(authorall);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching Book data: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching Book data");
            }
            

            
        }
        [HttpGet("Get-Id")]
        public async Task<IActionResult> GetById(int id)
        {
            //var author = _dbcontext.Authors.Where(b => b.Id == id);
            //if (author == null)
            //{
            //    return NotFound();
            //}
            //var authorDTO = author.Select(author => new AuthorDTO()
            //{
            //    Id = author.Id,
            //    FullName = author.FullName,
            //    NameBook = author.Book_Author.Select(ba => ba.books.Title).ToList(),
            //});
            //return StatusCode(StatusCodes.Status200OK, authorDTO);
            var getidauthor = _authorRepository.GetAuthorById(id);
            return Ok(getidauthor);

        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorDTO addAuthor)
        {
            //var author = new Authors
            //{
            //    FullName = addAuthor.FullName,
            //};
            //_dbcontext.Authors.Add(author);
            //await _dbcontext.SaveChangesAsync();
            //foreach (var id in addAuthor.BookIds)
            //{
            //    var book_author = new Book_Author()
            //    {
            //        BookId = author.Id,
            //        AuthorId = id
            //    };
            //}
            //return StatusCode(StatusCodes.Status200OK, author);
            var addauthor = _authorRepository.AddAuthor(addAuthor);
            return Ok(addauthor);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AddAuthorDTO authorDTO)
        {

            //var author = await _dbcontext.Authors.FindAsync(id);
            //if (author == null)
            //{
            //    return NotFound();
            //}
            //author.FullName = authorDTO.FullName;
            //_dbcontext.SaveChanges();
            //var book = _dbcontext.BooksAuthor.Where(a => a.AuthorId == id).ToList();
            //if (author != null)
            //{
            //    _dbcontext.BooksAuthor.RemoveRange(book);
            //    _dbcontext.SaveChanges();
            //}
            //foreach (var authorid in authorDTO.BookIds)
            //{
            //    var book_author = new Book_Author()
            //    {
            //        BookId = id,
            //        AuthorId = authorid,    
            //    };
            //}
            //return StatusCode(StatusCodes.Status200OK, author);
            var updateauthor = _authorRepository.UpdateAuthorById(id, authorDTO);
            return Ok(updateauthor);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            //var author = await _dbcontext.Authors.FindAsync(id);
            //if (author == null)
            //{
            //    return NotFound();
            //}
            //_dbcontext.Authors.Remove(author);
            //await _dbcontext.SaveChangesAsync();
            //return StatusCode(StatusCodes.Status200OK, author);
            var deteleauthor = _authorRepository.DeleteAuthorById(id);
            return Ok(deteleauthor);
        }
    }
    
}
