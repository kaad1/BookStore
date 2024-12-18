using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions <AuthDbContext>options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);

            var readerRoleId = "3228c3d3-1b21-44d1-8971-91ae16f184f3";
            var writerRoleId = "d50eade5-7564-42f4-b731-712375851588";
            //Create Reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp=readerRoleId
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId   
                }
            };


            //Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUserId = "3a76cecc-9988-4f3e-b1da-498e8945cab5";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@bookstore.com",
                Email = "admin@bookstore.com",
                NormalizedEmail = "admin@bookstore.com".ToUpper(),
                NormalizedUserName = "admin@bookstore.com".ToUpper()


            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
            builder.Entity<IdentityUser>().HasData(admin);


            //Give roles to admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId=adminUserId,
                    RoleId=readerRoleId

                },
                new()
                {
                    UserId=adminUserId,
                    RoleId=writerRoleId

                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles); 
        }
    }
}
