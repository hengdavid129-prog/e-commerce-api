using E_Commerce_api.DTO;
using E_Commerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_api.Services
{
    public class CategoryService(AppDBContext _db) : ICategoryService
    {
        public async Task<BaseResponse<CategoryResponseDTO>> Create(CategoryRequestDTO requestDTO)
        {
            var newCate = new Category
            {
                Name = requestDTO.Name,
                Description = requestDTO.Description,
                Created_At = DateTime.Now,
            };

            var result = _db.Add(newCate);
            await _db.SaveChangesAsync();

            if (result is null)
            {
                return BaseResponse<CategoryResponseDTO>.Failure("Failed to create category");
            }

            var data = new CategoryResponseDTO
            {
                Name = newCate.Name,
                Description = newCate.Description,
                Created_At = newCate.Created_At,
            };
            
            return BaseResponse<CategoryResponseDTO>.Success(data);
        }

        public async Task<BaseResponse<CategoryResponseDTO>> Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return BaseResponse<CategoryResponseDTO>.Failure("Category not found");
            }

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return BaseResponse<CategoryResponseDTO>.Success("Deleted Success");
        }

        public async Task<BaseResponse<List<CategoryResponseDTO>>> Get()
        {
            var categories = await _db.Categories
                                      .Select(cate => new CategoryResponseDTO
                                      {
                                          Name = cate.Name,
                                          Description = cate.Description,
                                          Created_At = cate.Created_At
                                      })
                                      .ToListAsync();

            return BaseResponse<List<CategoryResponseDTO>>.Success(categories);
        }


        public async Task<BaseResponse<CategoryResponseDTO>> Update(CategoryRequestDTO requestDTO)
        {
            var category = await _db.Categories.FindAsync(requestDTO.Id);

            if (category == null)
            {
                return BaseResponse<CategoryResponseDTO>.Failure("Category not found");
            }

            category.Name = requestDTO.Name;
            category.Description = requestDTO.Description;
            category.Updated_At = DateTime.Now;

            await _db.SaveChangesAsync();

            var data = new CategoryResponseDTO
            {
                Name = category.Name,
                Description = category.Description,
                Created_At = category.Created_At
            };

            return BaseResponse<CategoryResponseDTO>.Success(data);
        }
    }
}
