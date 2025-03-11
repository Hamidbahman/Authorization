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
    private readonly AuthDbContext dbContext;

    public RoleRepository(AuthDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

        public Task AddAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetByApplicationIdAsync(long applicationId)
        {
            throw new NotImplementedException();
        }

        public Task<Role?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetByStatusAsync(StatusTypes status)
        {
            throw new NotImplementedException();
        }

        public Task<Role?> GetByUuidAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Permission>> GetPermissionsByRoleIdsAsync(List<long> roleIds)
        {
            throw new NotImplementedException();
        }

        public async Task<Role?> GetRoleByIdAsync(long roleId)
    {
        return await dbContext.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == roleId);
    }

        public Task<List<Role>> GetRolesByApplicationIdAsync(long applicationId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetRolesByUserIdAsync(long userId)
    {
        return await dbContext.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .Include(r => r.Permissions)
            .ToListAsync();
    }

        public Task UpdateAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
