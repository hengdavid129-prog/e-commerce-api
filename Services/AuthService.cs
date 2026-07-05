using E_Commerce_api.DTO;
using E_Commerce_api.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_api.Services
{
    public class AuthService(UserManager<AppUser> _userManager, IConfiguration _config) : IAuthService
    {
        public async Task<BaseResponse<AuthResponseDTO>> Login(AuthRequestDTO requestDTO)
        {
            var user = await _userManager.FindByEmailAsync(requestDTO.Email);

            if (user is null)
            {
                return BaseResponse<AuthResponseDTO>.Failure("Invalid Credentials");
            }

            var isPasswordVarified = await _userManager.CheckPasswordAsync(user,requestDTO.Password);

            if (!isPasswordVarified)
            {
                return BaseResponse<AuthResponseDTO>.Failure("Invalid Credentails");
            }

            var data = new AuthResponseDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
            };
            return BaseResponse<AuthResponseDTO>.Success(data);
        }

        public async Task<BaseResponse<UserResponseDTO>> Resgister(UserRequestDTO userDTO)
        {
            var newUser = new AppUser
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserName = userDTO.Username,
                Email = userDTO.Email,
                DateOfBith = userDTO.DateOfBirth,
                CreatedAt = userDTO.CreatedAt,
            };

            var result = await _userManager.CreateAsync(newUser, userDTO.Password);

            if (result.Succeeded)
            {
                var data = new UserResponseDTO
                {
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                };
                return BaseResponse<UserResponseDTO>.Success(data);
            }

            var error = string.Join("\n", result.Errors.Select(e => e.Description));

            return BaseResponse<UserResponseDTO>.Failure(error);
        }
    }
}
