using Microsoft.AspNetCore.Identity;

namespace E_Commerce_api.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
