using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Commerce_api.DTO;
using E_Commerce_api.Models;
using E_Commerce_api.Utils;
using E_Commerce_api.Utils.QueryParams;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using System.Runtime.InteropServices;

namespace E_Commerce_api.Services
{
    public class ProductService(AppDBContext _db, IMapper _mapper) : IProductService
    {
        public async Task<BaseResponse<ProductResponseDTO>> Create(ProductRequestDTO dto)
        {
            if (HelperClass.isInvalidName(dto.Name))
            {
                return BaseResponse<ProductResponseDTO>.Failure("Please enter valid name.");
            }

            if (dto.StockQuantity < 0)
            {
                return BaseResponse<ProductResponseDTO>.Failure("Product stock cannot be less than 0.");
            }

            if (dto.Price < 0)
            {
                return BaseResponse<ProductResponseDTO>.Failure("Price cannot be less than 0.");
            }

            Product newProduct = _mapper.Map<Product>(dto);

            newProduct.CreatedAt = DateTime.Now;
            newProduct.UpdatedAt = DateTime.Now;

            _db.Products.Add(newProduct);
            await _db.SaveChangesAsync();

            var data = _mapper.Map<ProductResponseDTO>(newProduct);
            return BaseResponse<ProductResponseDTO>.Success(data);
        }

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return BaseResponse<bool>.Failure("Product not exist.");
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return BaseResponse<bool>.Success(true);
        }

        public async Task<BaseResponse<List<ProductResponseDTO>>> GetAll(ProductQueryParams queryParams)
        {
            // create query
            var query = _db.Products.AsQueryable();

            // to search by name
            if (!string.IsNullOrEmpty(queryParams.SearchText))
            {
                query = query.Where(p => p.Name.Contains(queryParams.SearchText));
            }

            // search by category
            if (queryParams.CategoryId > 0)
            {
                query = query.Where(p => p.CategoryId == queryParams.CategoryId);
            }

            /* will enable this when Category.cs is merged
            // this checks whether the categoryid provided exist in the db, if not exist in db, execute line in {}
            if (!await _db.Categories.AnyAsync(c => c.Id == item.CategoryId))
            {
                return BaseResponse<ProductResponseDTO>.Failure("Category does not exist");
            }
            */

            // sorting
            switch (queryParams.Sort.ToLower())
            {
                case "name":
                    query = query.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(p => p.Name);
                    break;
                case "price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.Price);
                    break;

                default:
                    break;
            }

            // count the total
            var totalRecord = await query.CountAsync();

            // PAGINATION
            var itemsToSkip = (queryParams.Page - 1) * GlobalConstants.PageSize;

            var data = await query
                .Skip(itemsToSkip)
                .Take(GlobalConstants.PageSize)
                .ProjectTo<ProductResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var metaData = new ListMetaData
            {
                TotalCount = totalRecord,
                PageNumber = queryParams.Page
            };

            return BaseResponse<List<ProductResponseDTO>>.Success(data);
        }

        public async Task<BaseResponse<ProductResponseDTO>> GetById(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return BaseResponse<ProductResponseDTO>.Failure("Product not exist.");
            }

            var data = _mapper.Map<ProductResponseDTO>(product);

            return BaseResponse<ProductResponseDTO>.Success(data);
        }

        public async Task<BaseResponse<ProductResponseDTO>> Update(int id, ProductRequestDTO dto)
        {
            if (HelperClass.isInvalidName(dto.Name))
            {
                return BaseResponse<ProductResponseDTO>.Failure("Please enter valid name.");
            }

            if (dto.StockQuantity < 0)
            {
                return BaseResponse<ProductResponseDTO>.Failure("Product stock cannot be less than 0.");
            }

            if (dto.Price < 0)
            {
                return BaseResponse<ProductResponseDTO>.Failure("Price cannot be less than 0.");
            }

            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return BaseResponse<ProductResponseDTO>.Failure("Product not exist");
            }

            _mapper.Map(dto, product);
            product.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();

            var data = _mapper.Map<ProductResponseDTO>(product);
            return BaseResponse<ProductResponseDTO>.Success(data);
        }
    }
}
