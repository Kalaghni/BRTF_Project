using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BRTF_Booking.Data
{
    public static class ApplicationSeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            //Create Roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Top-Level Admin", "Admin", "User" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //Create Users
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if (userManager.FindByEmailAsync("topleveladmin@niagaracollege.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "tladmin",
                    Email = "topleveladmin@niagaracollege.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Top-Level Admin").Wait();
                }
            }
            if (userManager.FindByEmailAsync("admin@niagaracollege.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@niagaracollege.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            if (userManager.FindByEmailAsync("user@niagaracollege.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "user",
                    Email = "user@niagaracollege.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }
    }
}
