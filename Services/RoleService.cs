using E_Commerce_api.DTO;
using E_Commerce_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_api.Services
{
    public class RoleService(RoleManager<AppRole> _roleManager, IConfiguration _config) : IRoleService
    {
        public async Task<BaseResponse<RoleResponseDTO>> Create(RoleRequestDTO requestDTO)
        {
            var newRole = new AppRole
            {
                Name = requestDTO.Name,
            };

            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                var data = new RoleResponseDTO
                {
                    Id = newRole.Id,
                    Name = newRole.Name
                };

                return BaseResponse<RoleResponseDTO>.Success(data);
            }

            var error = string.Join("\n", result.Errors.Select(e => e.Description));
            Console.WriteLine(error);
            return BaseResponse<RoleResponseDTO>.Failure(error);
        }

        public async Task<BaseResponse<RoleResponseDTO>> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is not null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    var data = new RoleResponseDTO
                    {
                        Id = role.Id,
                        Name = role.Name,
                        CreatedAt = role.CreatedAt,
                        UpdatedAt = role.UpdatedAt,
                    };
                    return BaseResponse<RoleResponseDTO>.Success(data);
                }
                var error = string.Join("\n", result.Errors.Select(e => e.Description));
                return BaseResponse<RoleResponseDTO>.Failure(error);
            }
            return BaseResponse<RoleResponseDTO>.Failure("Role not found.");
        }

        public async Task<BaseResponse<RoleResponseDTO>> FindById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is not null)
            {
                var data = new RoleResponseDTO
                {
                    Id = role.Id,
                    Name = role.Name,
                    CreatedAt = role.CreatedAt,
                    UpdatedAt = role.UpdatedAt,
                };
                return BaseResponse<RoleResponseDTO>.Success(data);
            }
            return BaseResponse<RoleResponseDTO>.Failure("Role not found.");
        }

        public async Task<BaseResponse<List<RoleResponseDTO>>> Get()
        {
            var allRoles = await _roleManager.Roles.ToListAsync();

            if (allRoles.Any())
            {
                var data = allRoles.Select(role => new RoleResponseDTO
                {
                    Id = role.Id,
                    Name = role.Name,
                    CreatedAt = role.CreatedAt,
                    UpdatedAt = role.UpdatedAt,
                }).ToList();

                return BaseResponse<List<RoleResponseDTO>>.Success(data);
            }

            return BaseResponse<List<RoleResponseDTO>>.Failure("No roles found.");
        }

        public async Task<BaseResponse<RoleResponseDTO>> Update(string id, RoleRequestDTO requestDTO)
        {
            var role = await _roleManager.FindByIdAsync(id);
            
            if (role is not null)
            {
                role.Name = requestDTO.Name;
                role.UpdatedAt = DateTime.UtcNow;

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    var data = new RoleResponseDTO
                    {
                        Id = role.Id,
                        Name = role.Name,
                        CreatedAt = role.CreatedAt,
                        UpdatedAt = role.UpdatedAt
                    };

                    return BaseResponse<RoleResponseDTO>.Success(data);
                }
                var error = string.Join("\n", result.Errors.Select(e => e.Description));
                return BaseResponse<RoleResponseDTO>.Failure(error);
            }
            return BaseResponse<RoleResponseDTO>.Failure("Role not found!");
        }
    }
}
