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
                new Role { Name = "Admin" },
                new Role { Name = "User" }
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


    }
}
