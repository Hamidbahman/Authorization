using System;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApplicationPackageRepository : IApplicationPackageRepository
{
    private readonly AuthorizationDbContext _context;
    public ApplicationPackageRepository(AuthorizationDbContext context)
    {
        _context = context;
    }
    public async Task<List<long>> GetApplicationPackageIdsByApplicationId(long applicationId)
    {
        return await  _context.ApplicationPackages
            .Where(ap => ap.ApplicationId == applicationId)
            .Select(ap => ap.Id)
            .ToListAsync();
    }


}
