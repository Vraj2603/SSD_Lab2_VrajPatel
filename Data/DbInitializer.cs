using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using SSD_Lab1.Models;   

namespace SSD_Lab1.Data   
{
    public static class DbInitializer
   
    {
        public static AppSecrets appSecrets { get; set; }
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
           
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

         
            if (roleManager.Roles.Any())
                return 1;

            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0) return 2;

            // Check if users already exist
            if (userManager.Users.Any())
                return 3;

            // Seed users
            result = await SeedUsers(userManager);
            if (result != 0) return 4;

            return 0;
        }

        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Supervisor role
            var result = await roleManager.CreateAsync(new IdentityRole("Supervisor"));
            if (!result.Succeeded)
                return 1;

            // Create Employee role
            result = await roleManager.CreateAsync(new IdentityRole("Employee"));
            if (!result.Succeeded)
                return 2;

            return 0;
        }

        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Supervisor user
            var supervisorUser = new ApplicationUser
            {
                UserName = "supervisor@test.com",
                Email = "supervisor@test.com",
                FirstName = "The",
                LastName = "Supervisor",
                City = "Hamilton",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(supervisorUser, appSecrets.AdminPassword);
            if (!result.Succeeded)
                return 1;
            result = await userManager.AddToRoleAsync(supervisorUser, "Supervisor");
            if (!result.Succeeded)
                return 2;

            // Employee user
            var employeeUser = new ApplicationUser
            {
                UserName = "employee@test.com",
                Email = "employee@test.com",
                FirstName = "",
                LastName = "EmpLoyee",
                City = "Toronto",
                EmailConfirmed = true
            };
            result = await userManager.CreateAsync(employeeUser, appSecrets.EmployeePassword);
            if (!result.Succeeded)
                return 3;
            result = await userManager.AddToRoleAsync(employeeUser, "Employee");
            if (!result.Succeeded)
                return 4;

            return 0;
        }
    }
}
