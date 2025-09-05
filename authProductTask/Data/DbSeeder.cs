using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CRUD_Operations.Models;

namespace CRUD_Operations.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // 1. Seed Roles
            string[] roles = new[] { "admin", "user", "productCreator" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2. Seed Admin
            var adminEmail = "admin@site.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "System",
                    LastName = "Admin"
                };
                await userManager.CreateAsync(admin, "Admin@123!");
                await userManager.AddToRoleAsync(admin, "admin");
            }

            // 3. Seed Default Product Creator
            var creatorEmail = "creator@site.com";
            var creator = await userManager.FindByEmailAsync(creatorEmail);
            if (creator == null)
            {
                creator = new ApplicationUser
                {
                    UserName = "creator",
                    Email = creatorEmail,
                    EmailConfirmed = true,
                    FirstName = "Product",
                    LastName = "Creator"
                };
                await userManager.CreateAsync(creator, "Creator@123!");
                await userManager.AddToRoleAsync(creator, "productCreator");
            }

            // 4. Seed Default User
            var userEmail = "user@site.com";
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "user",
                    Email = userEmail,
                    EmailConfirmed = true,
                    FirstName = "Normal",
                    LastName = "User"
                };
                await userManager.CreateAsync(user, "User@123!");
                await userManager.AddToRoleAsync(user, "user");
            }
        }
    }
}
