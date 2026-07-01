using Microsoft.EntityFrameworkCore;

namespace E_Commerce_api.Models
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options) 
    {
    }
}
