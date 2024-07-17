using ClayBackend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClayBackend.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorActivityLog> DoorActivityLogs { get; set; }
        public DbSet<DoorPermission> DoorPermissions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DoorPermission>().HasKey(m => new { m.DoorId, m.AuthorizedEntityId });

            builder.HasDefaultSchema("clay");
        }
    }
}
