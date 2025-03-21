using System;
using Domain.Entities;

namespace Domain;
public interface IApplicationRepository
{        Task<List<Aplication>> GetApplicationsByIdsAsync(List<long> applicationIds);

    Task<Aplication?> GetApplicationByIdAsync(long applicationId);

    Task<IEnumerable<Aplication>> GetAllAsync();
    Task<Aplication?> GetByIdAsync(long id);
    Task<Aplication?> GetByClientIdAsync(string clientId);
    Task<IEnumerable<Aplication>> GetByRoleIdAsync(long roleId);
    Task AddAsync(Aplication application);
    Task UpdateAsync(Aplication application);
    Task DeleteAsync(long id);
    Task<IEnumerable<Aplication>> GetApplicationsWithRolesAndPackagesAsync();
}
