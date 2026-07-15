using E_Commerce_api.DTO;

namespace E_Commerce_api.Services
{
    public interface IRoleService
    {
        Task<BaseResponse<List<RoleResponseDTO>>> Get();
        Task<BaseResponse<RoleResponseDTO>> FindById(string id);
        Task<BaseResponse<RoleResponseDTO>> Create(RoleRequestDTO requestDTO);
        Task<BaseResponse<RoleResponseDTO>> Update(string id ,RoleRequestDTO requestDTO);
        Task<BaseResponse<RoleResponseDTO>> Delete(string id);
    }
}
