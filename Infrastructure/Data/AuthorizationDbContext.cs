using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AuthorizationDbContext : DbContext
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Actee> Actees { get; set; }
        public DbSet<Aplication> Applications { get; set; }  
        public DbSet<ApplicationPackage> ApplicationPackages { get; set; }
        public DbSet<Mask> Masks { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<ApplicationPackage>()
                .HasOne(ap => ap.Application)
                .WithMany(a => a.ApplicationPackages)
                .HasForeignKey(ap => ap.ApplicationId);

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Actee)
                .WithMany()
                .HasForeignKey(m => m.ActeeId);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.Application)
                .WithMany(a => a.Roles)
                .HasForeignKey(r => r.ApplicationId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(ur => ur.UserId);
        }
    }
}
