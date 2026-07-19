using E_Commerce_api.DTO;
using E_Commerce_api.Services;
using E_Commerce_api.Utils.QueryParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace E_Commerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        // Get all
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductQueryParams queryParams)
        {
            var response = await _productService.GetAll(queryParams);

            if (!response.isSuccess) return BadRequest(response);

            return Ok(response);
        }

        // GetbyId
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _productService.GetById(id);

            if (!response.isSuccess) return BadRequest(response);

            return Ok(response);
        }

        // Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] ProductRequestDTO item)
        {
            var response = await _productService.Create(item);

            if (!response.isSuccess) return BadRequest(Response);

            return Ok(response);
        }

        // Update
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductRequestDTO item)
        {
            var response = await _productService.Update(id, item);

            if (!response.isSuccess) return BadRequest(Response);

            return Ok(response);
        }

        // Delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.Delete(id);

            if (!response.isSuccess) return BadRequest(Response);

            return Ok(response);
        }
    }
}
