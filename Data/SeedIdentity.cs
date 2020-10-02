using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBlogApi.Data
{
    public class SeedIdentity
    {
        public static async Task EnsurePopulated(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await CreateRoles(roleManager);
            await CreateUsers(userManager);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> rManager)
        {
            if (!await rManager.RoleExistsAsync("Manager"))
            {
                await rManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await rManager.RoleExistsAsync("User"))
            {
                await rManager.CreateAsync(new IdentityRole("User"));
            }
            if (!await rManager.RoleExistsAsync("Writer"))
            {
                await rManager.CreateAsync(new IdentityRole("Writer"));
            }
        }

        private static async Task CreateUsers(UserManager<IdentityUser> uManager)
        {
            IdentityUser Admin = new IdentityUser("christianfanell@icloud.com");

            await uManager.CreateAsync(Admin, "Secretpass1!");
            await uManager.AddToRoleAsync(Admin, "Admin");
            await uManager.AddToRoleAsync(Admin, "Writer");


        }
    }
}
