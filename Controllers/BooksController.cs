using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SzabóFlórabackend.Models.Dtos;
using SzabóFlórabackend.Services;
using SzabóFlórabackend.Services.ILibrary;

namespace SzabóFlórabackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBook _books;

        public BooksController(IBook books)
        {
            _books = books;
        }

        [HttpGet("feladat10")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _books.GetAllBooks();

            if (result.result != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("feladat13")]
        public async Task<IActionResult> AddBook([FromBody] BookDto newBook, [FromQuery] string userId)
        {
           
            var uid = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["UID"];

            var result = await _books.AddBook(newBook, userId, uid);

            if (result.result == null && result.message == "Nincs jogosultsága új könyv felvételéhez!")
            {
                return Unauthorized(result);
            }

            if (result.result != null)
            {
                return Created("", result); 
            }

            return BadRequest(result);
        }


    }
}
