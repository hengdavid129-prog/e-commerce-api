using AutoMapper;
using E_Commerce_api.DTO;
using E_Commerce_api.Models;

namespace E_Commerce_api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestDTO, Product>();
            CreateMap<Product, ProductResponseDTO>();
        }
    }
}
