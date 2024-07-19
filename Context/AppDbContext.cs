using ClayBackend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;

namespace ClayBackend.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Door> Doors { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("clay");

            builder.Entity<UserPermission>().HasKey(m => new { m.DoorId, m.UserId });
            builder.Entity<GroupPermission>().HasKey(m => new { m.DoorId, m.GroupId });
            builder.Entity<GroupMembership>().HasKey(m => new { m.GroupId, m.UserId });

            builder.Entity<ActivityLog>().HasIndex(a => new { a.DoorId, a.TimeStamp }).IsDescending(false, true);
            builder.Entity<Door>().HasIndex(d => d.Description);
            builder.Entity<Group>().HasIndex(g => g.Name);
            builder.Entity<User>().HasIndex(u => u.UserName);

            builder.Entity<User>()
            .HasMany(u => u.Groups)
            .WithMany(g => g.Members)
            .UsingEntity<GroupMembership>(
                j => j
                    .HasOne(gm => gm.Group)
                    .WithMany(g => g.Memberships)
                    .HasForeignKey(gm => gm.GroupId),
                j => j
                    .HasOne(gm => gm.User)
                    .WithMany(u => u.GroupMemberships)
                    .HasForeignKey(gm => gm.UserId),
                j =>
                {
                    j.HasKey(t => new { t.UserId, t.GroupId });
                }
            );

            builder.Entity<Group>()
                .HasMany<GroupMembership>(g => g.Memberships)
                .WithOne(gm => gm.Group)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                .HasMany<GroupPermission>(g => g.Permissions)
                .WithOne(gm => gm.Group)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<User>()
                .HasMany<GroupMembership>(u => u.GroupMemberships)
                .WithOne(gm => gm.User)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany<UserPermission>(u => u.Permissions)
                .WithOne(gm => gm.User)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany<ActivityLog>(u => u.ActivityLogs)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.Entity<Door>()
                .HasMany<UserPermission>(d => d.UserPermissions)
                .WithOne(p => p.Door)
                .HasForeignKey(p => p.DoorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Door>()
                .HasMany<GroupPermission>(d => d.GroupPermission)
                .WithOne(p => p.Door)
                .HasForeignKey(p => p.DoorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Door>()
                .HasMany<ActivityLog>(d => d.ActivityLogs)
                .WithOne(a => a.Door)
                .HasForeignKey(a => a.DoorId);

        }
    }
}
