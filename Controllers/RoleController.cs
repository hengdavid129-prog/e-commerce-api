using E_Commerce_api.DTO;
using E_Commerce_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleService _roleService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var result = await _roleService.Get();

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);  
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _roleService.FindById(id.ToString());
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] RoleRequestDTO requestDTO)
        {
            var result = await _roleService.Create(requestDTO);

            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(string id, [FromBody] RoleRequestDTO requestDTO)
        {
            var result = await _roleService.Update(id, requestDTO);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _roleService.Delete(id);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
