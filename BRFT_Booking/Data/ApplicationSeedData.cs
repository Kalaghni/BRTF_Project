using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRTF_Booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BRTF_Booking.Data
{
    public static class ApplicationSeedData
    {
        public static async Task SeedAsync(BRTFContext context, ApplicationDbContext identityContext, IServiceProvider serviceProvider)
        {
            //Create Roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Top-Level Admin", "Admin", "Student" };
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

            //Grab existing users
            var students = context.Users.ToList();


            foreach (User student in students)
            {
                if (userManager.FindByEmailAsync(student.Email).Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = student.Email,
                        Email = student.Email
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Student").Wait();
                    }
                }
            }

            var admins = context.Admins.ToList();

            foreach (Admin admin in admins)
            {
                if (userManager.FindByEmailAsync(admin.Email).Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = admin.Email,
                        Email = admin.Email
                    };

                    IdentityResult result = userManager.CreateAsync(user, "password").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, admin.Role).Wait();
                    }
                }
            }
        }
    }
}
