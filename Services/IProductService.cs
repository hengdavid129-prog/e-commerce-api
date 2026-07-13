using E_Commerce_api.DTO;
using E_Commerce_api.Utils.QueryParams;

namespace E_Commerce_api.Services
{
    public interface IProductService
    {
        Task<BaseResponse<List<ProductResponseDTO>>> GetAll(ProductQueryParams queryParams);
        Task<BaseResponse<ProductResponseDTO>> GetById(int id);
        Task<BaseResponse<ProductResponseDTO>> Create(ProductRequestDTO dto);
        Task<BaseResponse<ProductResponseDTO>> Update(int id, ProductRequestDTO dto);
        Task<BaseResponse<bool>> Delete(int id);
    }
}
