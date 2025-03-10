using System;

namespace Domain;

using Domain.Entities;

using Domain.Entities;

public interface IMaskRepository
{
    Task<List<Mask>> GetAllAsync();
    Task<Mask?> GetByIdAsync(int maskId);
    Task<List<Mask>> GetByPermissionIdAsync(long permissionId);
    Task<List<Mask>> GetByPermissionIdsAsync(List<long> permissionIds);
    Task AddAsync(Mask mask);
    Task AddRangeAsync(List<Mask> masks);
    Task UpdateAsync(Mask mask);
    Task DeleteAsync(int maskId);
    Task<bool> ExistsAsync(int maskId);
    Task<List<Mask>> GetMasksByPermissionsAsync(List<long> permissionIds);

}
