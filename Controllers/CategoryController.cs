using E_Commerce_api.DTO;
using E_Commerce_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(ICategoryService _categoryService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryService.Get());
        }

        // /api/category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] int id)
        {
            return Ok(await _categoryService.FindById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDTO requestDTO)
        {
            return Ok(await _categoryService.Create(requestDTO));
        }

        // /api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryRequestDTO requestDTO)
        {
            return Ok(await _categoryService.Update(id, requestDTO));
        }

        // /api/category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _categoryService.Delete(id));
        }
    }
}
