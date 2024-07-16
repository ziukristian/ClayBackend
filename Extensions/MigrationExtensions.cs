using ClayBackend.Context;
using ClayBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClayBackend.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }

        public static void SeedRoles(this IApplicationBuilder app)
        {
            var roles = new List<Role>
            {
                new Role { Name = "Admin", AccessLevel = -1 },
                new Role { Name = "User", AccessLevel = 0 }
            };
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using RoleManager<Role> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            if (!roleManager.Roles.Any())
            {
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }


        public static void SeedAdminUser(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            User admin = userManager.FindByEmailAsync("admin@clay.com").Result;
            if (admin == null)
            {
                admin = new User
                {
                    UserName = "admin@clay.com",
                    Email = "admin@clay.com",
                    EmailConfirmed = true
                };
                userManager.CreateAsync(admin, "Admin123!").Wait();
                userManager.AddToRoleAsync(admin, "Admin").Wait();
            }
        }


    }
}
