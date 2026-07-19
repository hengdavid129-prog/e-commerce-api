using E_Commerce_api.DTO;
using E_Commerce_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO userRequestDTO)
        {
            var result = await _authService.Resgister(userRequestDTO);
            
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDTO authRequestDTO)
        {
            var result = await _authService.Login(authRequestDTO);

            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
