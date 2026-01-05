using SzabóFlórabackend.Models.Dtos;

namespace SzabóFlórabackend.Services.ILibrary
{
    public interface IAuthors
    {
        Task<ResultDto> GetAuthorWithBooks(string name);
        Task<ResultDto> GetAuthorsCount();

    }
}
