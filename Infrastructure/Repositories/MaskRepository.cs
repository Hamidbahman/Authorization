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
    private readonly AuthDbContext dbContext;

    public MaskRepository(AuthDbContext dbContext)
    {
        this.dbContext = dbContext;
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

        public async Task<List<Mask>> GetMasksByPermissionIdsAsync(List<long> permissionIds)
    {
        return await dbContext.Masks
            .Where(m => permissionIds.Contains(m.PermissionId))
            .ToListAsync();
    }

        public Task<List<Mask>> GetMasksByPermissionsAsync(List<long> permissionIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<Mask>> GetMasksByUserIdAndClientIdAsync(long userId, string clientId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Mask mask)
        {
            throw new NotImplementedException();
        }
    }
}
