using Microsoft.EntityFrameworkCore;
using SzabóFlórabackend.Models;
using SzabóFlórabackend.Models.Dtos;
using SzabóFlórabackend.Services.ILibrary;

namespace SzabóFlórabackend.Services
{
    public class AuthorsService : IAuthors
    {
        private readonly LibrarydbContext _context;

        public AuthorsService(LibrarydbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> GetAuthorWithBooks(string name)
        {
            ResultDto resultDto = new ResultDto();

            try
            {
                var author = await _context.Authors
                    .Include(a => a.Books)
                    .Where(a => a.AuthorName == name)
                    .Select(a => new AuthorWithBooksDto
                    {
                        Id = a.AuthorId,
                        Name = a.AuthorName,
                        Books = a.Books.Select(b => new BookDto
                        {
                            Id = b.BookId,
                            Title = b.Title,
                            PublishDate = b.PublishDate,
                            AuthorId = b.AuthorId,
                            CategoryId = b.CategoryId
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (author == null)
                {
                    resultDto.message = "A megadott szerző nem található";
                    resultDto.result = null;
                    return resultDto;
                }

                resultDto.message = "Sikeres lekérdezés";
                resultDto.result = author;
                return resultDto;
            }
            catch (Exception ex)
            {
                resultDto.message = "Hiba történt: " + ex.Message;
                resultDto.result = null;
                return resultDto;
            }
        }

        public async Task<ResultDto> GetAuthorsCount()
        {
            ResultDto resultDto = new ResultDto();

            try
            {
                var count = await _context.Authors.CountAsync();

                resultDto.message = "Sikeres lekérdezés";
                resultDto.result = count;
                return resultDto;
            }
            catch (Exception ex)
            {
                resultDto.message = "Nem lehet csatlakozni az adatbázishoz";
                resultDto.result = null;
                return resultDto;
            }
        }
    }
}
