using E_Commerce_api.DTO;

namespace E_Commerce_api.Services
{
    public class AuthService : IAuthService
    {
        public Task<BaseResponse<AuthResponseDTO>> Login(AuthRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<AuthResponseDTO>> Resgister(AuthRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
