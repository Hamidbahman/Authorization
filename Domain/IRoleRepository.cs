using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(long id);
        Task<Role?> GetByUuidAsync(string uuid);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<IEnumerable<Role>> GetByApplicationIdAsync(long applicationId);
        Task<IEnumerable<Role>> GetByStatusAsync(StatusTypes status);
        Task<bool> ExistsAsync(string uuid);
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(long id);
    }
}
