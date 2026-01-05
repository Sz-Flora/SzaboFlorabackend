using SzabóFlórabackend.Models.Dtos;

namespace SzabóFlórabackend.Services.ILibrary
{
    public interface ICategory
    {
        Task<ResultDto> GetAllCategoriesWithBooks();
    }
}
