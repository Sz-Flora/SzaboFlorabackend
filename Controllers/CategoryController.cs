using Microsoft.AspNetCore.Mvc;
using SzabóFlórabackend.Services.ILibrary;

namespace SzabóFlórabackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _categoryService;

        public CategoriesController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("feladat11")]
        public async Task<IActionResult> GetAllCategoriesWithBooks()
        {
            var result = await _categoryService.GetAllCategoriesWithBooks();

            if (result.result != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
