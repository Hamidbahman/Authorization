using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AuthorizationDbContext _context;

        public PermissionRepository(AuthorizationDbContext context)
        {
            _context = context;
        }

        public async Task<Permission?> GetByIdAsync(long id)
        {
            return await _context.Permissions
                .Include(p => p.Role)
                .Include(p => p.Actee)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions
                .Include(p => p.Role)
                .Include(p => p.Actee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetByRoleIdAsync(long roleId)
        {
            return await _context.Permissions
                .Where(p => p.RoleId == roleId)
                .Include(p => p.Actee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetByActeeIdAsync(long acteeId)
        {
            return await _context.Permissions
                .Where(p => p.ActeeId == acteeId)
                .Include(p => p.Role)
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetByStatusAsync(StatusTypes status)
        {
            return await _context.Permissions
                .Where(p => p.Status == status)
                .Include(p => p.Role)
                .Include(p => p.Actee)
                .ToListAsync();
        }
        public List<long> GetPermissionIdsByRoles(List<Role> roles)
        {
            return roles.SelectMany(r => r.Permissions.Select(p => p.Id)).Distinct().ToList();
        }

        public async Task<List<Permission>> GetPermissionsByRolesAsync(List<long> roleIds)
        {
            return await _context.Set<Permission>()
                .Where(p => roleIds.Contains(p.RoleId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetByGrantingLevelAsync(int granting)
        {
            return await _context.Permissions
                .Where(p => p.Granting == granting)
                .Include(p => p.Role)
                .Include(p => p.Actee)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(long roleId, long acteeId)
        {
            return await _context.Permissions
                .AnyAsync(p => p.RoleId == roleId && p.ActeeId == acteeId);
        }

        public async Task AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }
        public Task<List<long>> GetPermissionIdsByRolesAsync(List<Role> roles)
        {
            var permissionIds = roles
                .SelectMany(r => r.Permissions.Select(p => p.Id))
                .Distinct()
                .ToList();

            return Task.FromResult(permissionIds);
    }

        public async Task<List<Mask>> GetMasksByPermissionIdsAsync(List<long> permissionIds)
{
    return await _context.Masks
        .Where(m => permissionIds.Contains(m.PermissionId))
        .ToListAsync();
}

    }
}
