using E_Commerce_api.DTO;
using E_Commerce_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService _categoryService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDTO requestDTO)
        {
            return Ok(await _categoryService.Create(requestDTO));
        }
    }
}
