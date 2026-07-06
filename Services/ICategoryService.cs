using E_Commerce_api.DTO;

namespace E_Commerce_api.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse<List<CategoryResponseDTO>>> Get();
        Task<BaseResponse<CategoryResponseDTO>> Create(CategoryRequestDTO requestDTO);
        Task<BaseResponse<CategoryResponseDTO>> Update(CategoryRequestDTO requestDTO);
        Task<BaseResponse<CategoryResponseDTO>> Delete(int id);
    }
}
