using SzabóFlórabackend.Models.Dtos;

namespace SzabóFlórabackend.Services.ILibrary
{
    public interface IBook
    {
        Task<ResultDto> GetAllBooks();
        Task<ResultDto> AddBook(BookDto newBook, string userId, string uidFromConfig);

    }
}
