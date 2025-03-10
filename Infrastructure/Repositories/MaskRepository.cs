using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using System.Net;
using Infrastructure.Data;
using Domain;

namespace Infrastructure.Repositories
{
    public class MaskRepository : IMaskRepository
    {
        private readonly AuthorizationDbContext _context;

        public MaskRepository(AuthorizationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Mask mask)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<Mask> masks)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int maskId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int maskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Mask>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Mask?> GetByIdAsync(int maskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Mask>> GetByPermissionIdAsync(long permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Mask>> GetByPermissionIdsAsync(List<long> permissionIds)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Mask>> GetMasksByPermissionsAsync(List<long> permissionIds)
        {
            return await _context.Set<Mask>()
                .Where(m => permissionIds.Contains(m.PermissionId))
                .ToListAsync();
        }
        
        public async Task<List<Mask>> GetMasksByUserIdAndClientIdAsync(long userId, string clientId)
        {
            return await _context.Masks
                .Where(m => _context.Permissions
                    .Where(p => _context.Roles
                        .Where(r => _context.UserRoles
                            .Where(ur => ur.UserId == userId)
                            .Select(ur => ur.RoleId)
                            .Contains(r.Id)
                        )
                        .Where(r => _context.Applications
                            .Where(a => a.ClientId == clientId)
                            .Select(a => a.Id)
                            .Contains(r.ApplicationId)
                        )
                        .Select(r => r.Id)
                        .Contains(p.RoleId)
                    )
                    .Select(p => p.Id)
                    .Contains(m.PermissionId)
                )
                .ToListAsync();
        }

        public Task UpdateAsync(Mask mask)
        {
            throw new NotImplementedException();
        }
    }
}
