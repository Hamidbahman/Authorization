using System;
using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApplicationPackageRepository : IApplicationPackageRepository
{
    private readonly AuthDbContext _context;
    public ApplicationPackageRepository(AuthDbContext context)
    {
        _context = context;
    }
    
    public async Task<ApplicationPackage?> GetPackageByApplicationIdAsync(long applicationId)
    {
        return await _context.ApplicationPackages
                             .FirstOrDefaultAsync(ap => ap.ApplicationId == applicationId);
    }
    public async Task<List<long>> GetApplicationPackageIdsByApplicationId(long applicationId)
    {
        return await  _context.ApplicationPackages
            .Where(ap => ap.ApplicationId == applicationId)
            .Select(ap => ap.Id)
            .ToListAsync();
    }


}
