
using System;
using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly AuthorizationDbContext _context;

    public MenuRepository(AuthorizationDbContext context)
    {
        _context = context;
    }

    public async Task<Menu?> GetByMenuKeyAsync(string menuKey)
    {
        return await _context.Menus.FirstOrDefaultAsync(m => m.MenuKey == menuKey);
    }

    public async Task<Menu?> GetByIdAsync(long id)
    {
        return await _context.Menus.FindAsync(id);
    }

    public async Task<List<Menu>> GetAllAsync()
    {
        return await _context.Menus.ToListAsync();
    }
    
    public async Task<List<string>> GetMenuKeysByActeeIdsAsync(List<long> acteeIds)
    {
    if (acteeIds == null || !acteeIds.Any())
        return new List<string>();

    return await _context.Menus
        .Where(m => acteeIds.Contains(m.ActeeId))
        .Select(m => m.MenuKey)
        .ToListAsync();
    }

    public async Task<List<Menu>> GetByActeeIdAsync(long acteeId)
    {
        return await _context.Menus.Where(m => m.ActeeId == acteeId).ToListAsync();
    }

    public async Task AddAsync(Menu menu)
    {
        await _context.Menus.AddAsync(menu);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Menu menu)
    {
        _context.Menus.Update(menu);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(string menuKey)
    {
        var menu = await GetByMenuKeyAsync(menuKey);
        if (menu != null)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
    }
}
