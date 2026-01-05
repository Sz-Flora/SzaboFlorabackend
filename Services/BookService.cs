using Microsoft.EntityFrameworkCore;
using SzabóFlórabackend.Models;
using SzabóFlórabackend.Models.Dtos;
using SzabóFlórabackend.Services.ILibrary;

namespace SzabóFlórabackend.Services
{
    public class BookService : IBook
    {
        private readonly LibrarydbContext _context;

        public BookService(LibrarydbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> GetAllBooks()
        {
            ResultDto resultDto = new ResultDto();

            try
            {
                var books = await _context.Books
                    .Select(b => new BookDto
                    {
                        Id = b.BookId,
                        Title = b.Title,
                        PublishDate = b.PublishDate,
                        AuthorId = b.AuthorId,
                        CategoryId = b.CategoryId
                    }).ToListAsync();

                resultDto.message = "Sikeres lekérdezés";
                resultDto.result = books;
                return resultDto;
            }
            catch (Exception ex)
            {
                resultDto.message = "Hiba történt: " + ex.Message;
                resultDto.result = null;
                return resultDto;
            }
        }

        public async Task<ResultDto> AddBook(BookDto newBook, string userId, string uidFromConfig)
        {
            ResultDto resultDto = new ResultDto();

            if (userId != uidFromConfig)
            {
                resultDto.message = "Nincs jogosultsága új könyv felvételéhez!";
                resultDto.result = null;
                return resultDto;
            }

            try
            {
                var book = new Book
                {
                    Title = newBook.Title,
                    PublishDate = newBook.PublishDate,
                    AuthorId = newBook.AuthorId,
                    CategoryId = newBook.CategoryId
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                resultDto.message = "Könyv hozzáadása sikeresen megtörtént.";
                resultDto.result = book;
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
