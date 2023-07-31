using JeniesStory.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Infrastructure.Seeder
{
    public class Seeders
    {
        public async static Task Seed(RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            await dbContext.Database.EnsureCreatedAsync();

            await SeedRoles(roleManager);

        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Seed roles if they don't exist
            var roles = new List<string> { "User", "Author", "Admin" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
