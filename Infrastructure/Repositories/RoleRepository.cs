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
    public class RoleRepository : IRoleRepository
    {
        private readonly AuthorizationDbContext _context;

        public RoleRepository(AuthorizationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRolesByApplicationId(long applicationId)
        {
            return _context.Roles
                .Where(r => r.ApplicationId == applicationId)
               .Include(r => r.Permissions)
               .ToList();
        }

        public async Task<Role?> GetByIdAsync(long id)
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role?> GetByUuidAsync(string uuid)
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Uuid == uuid);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .ToListAsync();
        }

        public async Task<List<Permission>> GetPermissionsByRoleIdsAsync(List<long> roleIds)
        {
            return await _context.Roles
                .Where(r => roleIds.Contains(r.Id))
                .SelectMany(r => r.Permissions)
                .ToListAsync();
        }




        public async Task<IEnumerable<Role>> GetByApplicationIdAsync(long applicationId)
        {
            return await _context.Roles
                .Where(r => r.ApplicationId == applicationId)
                .Include(r => r.Permissions)
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetByStatusAsync(StatusTypes status)
        {
            return await _context.Roles
                .Where(r => r.Status == status)
                .Include(r => r.Permissions)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string uuid)
        {
            return await _context.Roles.AnyAsync(r => r.Uuid == uuid);
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
