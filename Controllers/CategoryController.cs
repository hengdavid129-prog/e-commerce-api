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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return Ok(await _categoryService.Get());
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryRequestDTO requestDTO)
        {
            return Ok(await _categoryService.Update(id, requestDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _categoryService.Delete(id));
        }
    }
}
