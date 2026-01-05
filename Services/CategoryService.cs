using Microsoft.EntityFrameworkCore;
using SzabóFlórabackend.Models;
using SzabóFlórabackend.Models.Dtos;
using SzabóFlórabackend.Services.ILibrary;

namespace SzabóFlórabackend.Services
{
    public class CategoryService : ICategory
    {
        private readonly LibrarydbContext _context;

        public CategoryService(LibrarydbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> GetAllCategoriesWithBooks()
        {
            ResultDto resultDto = new ResultDto();

            try
            {
                var categories = await _context.Categories
                    .Include(c => c.Books)
                    .Select(c => new CategoryWithBooksDto
                    {
                        Id = c.CategoryId,
                        Name = c.CategoryName,
                        Books = c.Books.Select(b => new BookDto
                        {
                            Id = b.BookId,
                            Title = b.Title,
                            PublishDate = b.PublishDate,
                            AuthorId = b.AuthorId,
                            CategoryId = b.CategoryId
                        }).ToList()
                    })
                    .ToListAsync();

                resultDto.message = "Sikeres lekérdezés";
                resultDto.result = categories;
                return resultDto;
            }
            catch (Exception ex)
            {
                resultDto.message = "Hiba történt: " + ex.Message;
                resultDto.result = null;
                return resultDto;
            }
        }
    }
}
