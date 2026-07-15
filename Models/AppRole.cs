using Microsoft.AspNetCore.Identity;

namespace E_Commerce_api.Models
{
    public class AppRole : IdentityRole
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
