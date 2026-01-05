using Microsoft.AspNetCore.Mvc;
using SzabóFlórabackend.Services.ILibrary;

namespace SzabóFlórabackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthors _authors;

        public AuthorsController(IAuthors authors)
        {
            _authors = authors;
        }

        [HttpGet("feladat9/{name}")]
        public async Task<IActionResult> GetAuthorWithBooks(string name)
        {
            var result = await _authors.GetAuthorWithBooks(name);

            if (result.result != null)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("feladat12")]
        public async Task<IActionResult> GetAuthorsCount()
        {
            var result = await _authors.GetAuthorsCount();

            if (result.result != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
