using ClayBackend.Context;
using Microsoft.EntityFrameworkCore;

namespace ClayBackend.Extensions
{
    public static class MigrationExtensions
    {
        public static void Migrate(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}
