using E_Commerce_api.DTO;
using E_Commerce_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;

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

            var isPwdVarified = await _userManager.CheckPasswordAsync(user, requestDTO.Password);

            if (!isPwdVarified)
            {
                return BaseResponse<AuthResponseDTO>.Failure("Invalid Credentails");
            }

            // Login Success
            var KeyBytes =
            Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key is missing"));
            var securityKey = new SymmetricSecurityKey(KeyBytes);
            var credentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);

            var issuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("JJWT Issuer is missing");
            var audience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt Audience is missing");

            // 2. Define the User's Claims (The payload)

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault() ?? "User";

            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole),
            };

            // 3. Create the Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            // 4. Generate and return the string token
            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);

            var data = new AuthResponseDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                Token = token
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
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                };
                return BaseResponse<UserResponseDTO>.Success(data);
            }

            var error = string.Join("\n", result.Errors.Select(e => e.Description));
            Console.WriteLine($"Error: {error}");
            return BaseResponse<UserResponseDTO>.Failure(error);
        }
    }
}
