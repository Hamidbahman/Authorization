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

    public async Task<IEnumerable<Application>> GetAllAsync()
    {
        return await _context.Applications
            .AsSplitQuery() 
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .ToListAsync();
    }

    public async Task<Application?> GetByIdAsync(long id)
    {
        return await _context.Applications
            .AsSplitQuery()
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Application?> GetByClientIdAsync(string clientId)
    {
        return await _context.Applications
            .AsSplitQuery()
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .FirstOrDefaultAsync(a => a.ClientId == clientId);
    }

    public async Task<IEnumerable<Application>> GetByRoleIdAsync(long roleId)
    {
        return await _context.Applications
            .AsSplitQuery()
            .Where(a => a.Roles.Any(r => r.Id == roleId))
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .ToListAsync();
    }

    public async Task AddAsync(Application application)
    {
        await _context.Applications.AddAsync(application);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Application application)
    {
        _context.Applications.Update(application);
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

    public async Task<IEnumerable<Application>> GetApplicationsWithRolesAndPackagesAsync()
    {
        return await _context.Applications
            .AsSplitQuery()
            .Include(a => a.Roles)
            .Include(a => a.ApplicationPackages)
            .ToListAsync();
    }
}
