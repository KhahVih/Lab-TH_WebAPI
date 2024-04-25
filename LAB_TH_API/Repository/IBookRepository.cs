using LAB_TH_API.Models;
using LAB_TH_API.Models.DTO;

namespace LAB_TH_API.Services
{
    public interface IBookRepository
    {
        List<BookDTO> GetAllBooks();
        BookDTO GetBookById(int id);
        AddBookDTO AddBook(AddBookDTO addBookRequestDTO);
        AddBookDTO? UpdateBookById(int id, AddBookDTO bookDTO);
        Books? DeleteBookById(int id);
    }
}
