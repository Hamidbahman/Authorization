using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AuthorizationDbContext : DbContext
{
    public AuthorizationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users {get;set;}
    public DbSet<Role> Roles {get;set;}
    public DbSet<UserRole> UserRoles {get;set;}
    public DbSet<Application> Applications {get;set;}
    public DbSet<Actee> Actees {get;set;}
    public DbSet<Permission> Permissions {get;set;}
    public DbSet<Mask> Masks {get;set;}
    public DbSet<Menu> Menus {get;set;}
    public DbSet<ApplicationPackage> ApplicationPackages{get;set;}

}
