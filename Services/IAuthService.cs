using E_Commerce_api.DTO;

namespace E_Commerce_api.Services
{
    public interface IAuthService
    {
        Task<BaseResponse<AuthResponseDTO>> Resgister(AuthRequestDTO requestDTO);
        Task<BaseResponse<AuthResponseDTO>> Login(AuthRequestDTO requestDTO);
    }
}
