using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AuthorizationDbContext _context;

    public ApplicationRepository(AuthorizationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Aplication>> GetAllAsync()
    {
        return await _context.Applications
            .AsSplitQuery() 
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .ToListAsync();
    }

    public async Task<Aplication?> GetByIdAsync(long id)
    {
        return await _context.Applications
            .AsSplitQuery()
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Aplication?> GetByClientIdAsync(string clientId)
    {
        return await _context.Applications
            .AsSplitQuery()
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .FirstOrDefaultAsync(a => a.ClientId == clientId);
    }

    public async Task<IEnumerable<Aplication>> GetByRoleIdAsync(long roleId)
    {
        return await _context.Applications
            .AsSplitQuery()
            .Where(a => a.Roles.Any(r => r.Id == roleId))
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .ToListAsync();
    }

    public async Task AddAsync(Aplication aplication)
    {
        await _context.Applications.AddAsync(aplication);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aplication aplication)
    {
        _context.Applications.Update(aplication);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var application = await _context.Applications.FindAsync(id);
        if (application != null)
        {
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Aplication>> GetApplicationsWithRolesAndPackagesAsync()
    {
        return await _context.Applications
            .AsSplitQuery()
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .ToListAsync();
    }
}
