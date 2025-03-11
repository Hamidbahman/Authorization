
using System;
using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly AuthDbContext dbContext;

    public MenuRepository(AuthDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task AddAsync(Menu menu)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string menuKey)
    {
        throw new NotImplementedException();
    }

    public Task<List<Menu>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Menu>> GetByActeeIdAsync(long acteeId)
    {
        throw new NotImplementedException();
    }

    public Task<Menu?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Menu?> GetByMenuKeyAsync(string menuKey)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetMenuKeysByActeeIdsAsync(List<long> acteeIds)
    {
        return await dbContext.Menus
            .Where(m => acteeIds.Contains(m.ActeeId))
            .Select(m => m.MenuKey)
            .ToListAsync();
    }

    public Task UpdateAsync(Menu menu)
    {
        throw new NotImplementedException();
    }
}

