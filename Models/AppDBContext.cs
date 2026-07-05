using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_api.Models
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : IdentityDbContext<AppUser>(options) 
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string adminRoleId = "CF73A172-9EA1-46A6-9208-BEFFE1BA9208";
            string userRoleId = "014C2AAD-B1C0-47B0-9AEF-54FEA8F80E3B";
            string adminUserId = "20F75FF1-FBBA-4BFB-A433-93E55620B003";

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = adminRoleId
            }, new IdentityRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = userRoleId
            });

            var adminUser = new AppUser
            {
                Id = adminUserId,
                FirstName = "Heng",
                LastName = "David",
                UserName = "hengdavid",
                NormalizedUserName = "HENGDAVID",
                Email = "hengdavid@gmail.com",
                NormalizedEmail = "HENGDAVID@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAECiqFAsB2eGoSgzfBo+JD0crZT0IFSawL0G1tcczxieS75VdlkE05XIpI7mo3cq+iw==",
                PhoneNumber = "012345678",
                DateOfBith = new DateOnly(2000, 01, 01),
                CreatedAt = new DateTime(2026, 7, 5, 0, 0, 0, DateTimeKind.Utc),
                ConcurrencyStamp = adminUserId,
                SecurityStamp = "9A7164A9-52A6-4EBA-A548-3A0B006EC5BC",
            };

            builder.Entity<AppUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId,
            });
        }
    }
}
