using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        // DbSets for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Actee> Actees { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Mask> Masks { get; set; }
        public DbSet<ApplicationPackage> ApplicationPackages { get; set; }
        public DbSet<Aplication> Applications { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Fluent API for User
    modelBuilder.Entity<User>(entity =>
    {
        entity.HasKey(u => u.Id);
        entity.Property(u => u.Uuid).IsRequired().HasMaxLength(40);
        entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
        entity.Property(u => u.PhoneNumber).HasMaxLength(20);
        entity.Property(u => u.NationalCode).HasMaxLength(50);
        entity.Property(u => u.Description).HasMaxLength(1000);

        entity.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // Safe to cascade delete users

        entity.HasIndex(u => u.Email).IsUnique();
    });

    // Fluent API for Role
    modelBuilder.Entity<Role>(entity =>
    {
        entity.HasKey(r => r.Id);
        entity.Property(r => r.Uuid).IsRequired().HasMaxLength(40);
        entity.Property(r => r.Title).HasMaxLength(200);
        entity.Property(r => r.Description).HasMaxLength(1000);

        entity.HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.NoAction);  // Prevents cycle

        entity.HasMany(r => r.Permissions)
            .WithOne(p => p.Role)
            .HasForeignKey(p => p.RoleId)
            .OnDelete(DeleteBehavior.NoAction);  // Prevents cycle
    });

    // Fluent API for UserRole
    modelBuilder.Entity<UserRole>(entity =>
    {
        entity.HasKey(ur => ur.Id);

        entity.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.NoAction);  // Prevents cycle
    });

    // Fluent API for Permission
    modelBuilder.Entity<Permission>(entity =>
    {
        entity.HasKey(p => p.Id);

        entity.HasOne(p => p.Actee)
            .WithMany(a => a.Permissions)
            .HasForeignKey(p => p.ActeeId)
            .OnDelete(DeleteBehavior.Cascade); // Safe

        entity.HasOne(p => p.Role)
            .WithMany(r => r.Permissions)
            .HasForeignKey(p => p.RoleId)
            .OnDelete(DeleteBehavior.NoAction); // Prevents cycle
    });

    // Fluent API for Actee
    modelBuilder.Entity<Actee>(entity =>
    {
        entity.HasKey(a => a.Id);
        entity.Property(a => a.Uuid).IsRequired().HasMaxLength(40);
        entity.Property(a => a.Title).IsRequired().HasMaxLength(200);
        entity.Property(a => a.Description).HasMaxLength(1000);

        entity.HasMany(a => a.Permissions)
            .WithOne(p => p.Actee)
            .HasForeignKey(p => p.ActeeId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(a => a.Menus)
            .WithOne(m => m.Actee)
            .HasForeignKey(m => m.ActeeId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    // Fluent API for Menu
    modelBuilder.Entity<Menu>(entity =>
    {
        entity.HasKey(m => m.Id);
        entity.Property(m => m.MenuKey).IsRequired().HasMaxLength(100);
        entity.Property(m => m.Icon).HasMaxLength(100);

        entity.HasOne(m => m.Actee)
            .WithMany(a => a.Menus)
            .HasForeignKey(m => m.ActeeId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    // Fluent API for Mask
    modelBuilder.Entity<Mask>(entity =>
    {
        entity.HasKey(m => m.Id);

        entity.HasOne(m => m.Permission)
            .WithMany()
            .HasForeignKey(m => m.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    // Fluent API for ApplicationPackage
    modelBuilder.Entity<ApplicationPackage>(entity =>
    {
        entity.HasKey(ap => ap.Id);

        entity.HasOne(ap => ap.Application)
            .WithMany(a => a.ApplicationPackages)
            .HasForeignKey(ap => ap.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    // Fluent API for Aplication
    modelBuilder.Entity<Aplication>(entity =>
    {
        entity.HasKey(a => a.Id);

        entity.HasMany(a => a.Roles)
            .WithOne(r => r.Application)
            .HasForeignKey(r => r.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(a => a.ApplicationPackages)
            .WithOne(ap => ap.Application)
            .HasForeignKey(ap => ap.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    });



            // Seeding the tables with sample data

            // Seeding Users
            modelBuilder.Entity<User>().HasData(
                new User(
                    id: 1,
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null,
                    uuid: "user-1",
                    firstName: "John",
                    lastName: "Doe",
                    nationalCode: "1234567890",
                    email: "admin@example.com",
                    phoneNumber: "1234567890",
                    description: "Administrator User"
                )
            );

            // Seeding Applications
            modelBuilder.Entity<Aplication>().HasData(
                new Aplication(
                    applicationPackages: new List<ApplicationPackage>(),
                    roles: new List<Role>(),
                    id: 1,
                    title: "Main Application",
                    clientId: "client-1",
                    redirectUrls: "https://app.example.com",
                    clientScope: "read,write",
                    clientSecret: "secret-key",
                    authenticateGrantType: "password",
                    ipRange: "192.168.1.0/24",
                    isAutoApprove: true,
                    scheduled: "00:00-23:59",
                    status: 1,
                    lockEnabled: true,
                    description: "Primary Application",
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null
                )
            );

            // Seeding Roles
            modelBuilder.Entity<Role>().HasData(
                new Role(
                    id: 1,
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null,
                    uuid: "role-1",
                    authority: AuthorityType.All,
                    title: "Administrator",
                    description: "Admin role with full permissions",
                    status: StatusTypes.Active,
                    applicationId: 1,
                    isAdmin: true
                )
            );

            // Seeding UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole(
                    id: 1,
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null,
                    userId: 1,
                    roleId: 1,
                    isDefault: true
                )
            );

            // Seeding Permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission(
                    id: 1,
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null,
                    acteeId: 1,
                    roleId: 1,
                    status: StatusTypes.Active,
                    granting: 1
                )
            );

            // Seeding Application Packages
            modelBuilder.Entity<ApplicationPackage>().HasData(
                new ApplicationPackage(
                    id: 1,
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null,
                    title: "Basic Package",
                    applicationId: 1
                )
            );

            // Seeding Actees
            modelBuilder.Entity<Actee>().HasData(
                new Actee(
                    id: 1,
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null,
                    uuid: "actee-1",
                    acteeType: ActeeTypes.Menu,
                    title: "Admin Dashboard",
                    description: "Administrator Panel",
                    status: StatusTypes.Active,
                    applicationPackageId: 1
                )
            );

            // Seeding Menus
            modelBuilder.Entity<Menu>().HasData(
                new Menu(
                    id: 1,
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null,
                    menuKey: "menu-1",
                    priority: 1,
                    icon: "admin_icon",
                    acteeId: 1
                )
            );

            // Seeding Masks
            modelBuilder.Entity<Mask>().HasData(
                new Mask(
                    id: 1,
                    createDate: new DateTime(2023, 1, 1),
                    modifyDate: new DateTime(2023, 1, 1),
                    deleteDate: null,
                    deleteUser: null,
                    modifyUser: null,
                    permissionId: 1
                )
            );
        }
    }

