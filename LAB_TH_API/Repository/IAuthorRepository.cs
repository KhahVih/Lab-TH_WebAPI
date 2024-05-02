using LAB_TH_API.Models.DTO;
using LAB_TH_API.Models;

namespace LAB_TH_API.Repository
{
    public interface IAuthorRepository
    {
        List<AuthorDTO> GetAllAuthor();
        AuthorDTO GetAuthorById(int id);
        AddAuthorDTO AddAuthor(AddAuthorDTO addauthorDTO);
        AddAuthorDTO? UpdateAuthorById(int id, AddAuthorDTO authorDTO);
        Authors? DeleteAuthorById(int id);
    }
}
