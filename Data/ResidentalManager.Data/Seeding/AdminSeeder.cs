namespace ResidentalManager.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using ResidentalManager.Common;
    using ResidentalManager.Data.Models;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (dbContext.Users.Any(u => u.UserName == "admin@mail.com"))
            {
                return;
            }

            var admin = new Models.ApplicationUser()
            {
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(admin, "admin@mail.com");
            admin.PasswordHash = hashed;

            await userManager.CreateAsync(admin);
            await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
        }
    }
}
