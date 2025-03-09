using System;
using Domain.Entities;

namespace Domain;
public interface IApplicationRepository
{
    Task<IEnumerable<Application>> GetAllAsync();
    Task<Application?> GetByIdAsync(long id);
    Task<Application?> GetByClientIdAsync(string clientId);
    Task<IEnumerable<Application>> GetByRoleIdAsync(long roleId);
    Task AddAsync(Application application);
    Task UpdateAsync(Application application);
    Task DeleteAsync(long id);
    Task<IEnumerable<Application>> GetApplicationsWithRolesAndPackagesAsync();
}
