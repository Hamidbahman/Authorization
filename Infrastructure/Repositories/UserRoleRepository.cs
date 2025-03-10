using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;
public class UserRoleRepository : IUserRoleRepository
{
    private readonly AuthorizationDbContext _context;

    public UserRoleRepository(AuthorizationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await _context.UserRoles
            .AsSplitQuery()
            .Include(ur => ur.UserId)
            .Include(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<UserRole?> GetByIdAsync(long id)
    {
        return await _context.UserRoles
            .AsSplitQuery()
            .Include(ur => ur.UserId)
            .Include(ur => ur.Role)
            .FirstOrDefaultAsync(ur => ur.Id == id);
    }

    public async Task<IEnumerable<UserRole>> GetByUserIdAsync(long userId)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.RoleId)
            .ToListAsync();
    }
    public async Task<List<Role>> GetRolesByUserIdAsync(long userId)
    {
        return await (from userRole in _context.Set<UserRole>()
            join role in _context.Set<Role>() on userRole.RoleId equals role.Id
            where userRole.UserId == userId
            select role).ToListAsync();
    }

    public async Task<IEnumerable<UserRole>> GetByRoleIdAsync(long roleId)
    {
        return await _context.UserRoles
            .Where(ur => ur.RoleId == roleId)
            .Include(ur => ur.UserId)
            .ToListAsync();
    }

    public async Task AddAsync(UserRole userRole)
    {
        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserRole userRole)
    {
        _context.UserRoles.Update(userRole);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(long id)
    {
        var userRole = await _context.UserRoles.FindAsync(id);
        if (userRole != null)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }
    }
}
