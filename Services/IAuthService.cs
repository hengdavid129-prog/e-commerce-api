using E_Commerce_api.DTO;

namespace E_Commerce_api.Services
{
    public interface IAuthService
    {
        Task<BaseResponse<UserResponseDTO>> Resgister(UserRequestDTO requestDTO);
        Task<BaseResponse<AuthResponseDTO>> Login(AuthRequestDTO requestDTO);
    }
}
