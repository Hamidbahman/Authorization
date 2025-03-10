using System;
using Domain.Entities;

namespace Domain;

public interface IMenuRepository
{
    Task<Menu?> GetByMenuKeyAsync(string menuKey);
    Task<Menu?> GetByIdAsync(long id);
    Task<List<Menu>> GetAllAsync();
    Task<List<Menu>> GetByActeeIdAsync(long acteeId);
    Task AddAsync(Menu menu);
    Task UpdateAsync(Menu menu);
    Task DeleteAsync(string menuKey);

}
