using System;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ApplicationPackageRepository
{
    private readonly AuthorizationDbContext _context;
    public ApplicationPackageRepository(AuthorizationDbContext context)
    {
        _context = context;
    }
    public async Task<List<long>> GetApplicationPackageIdsByApplicationId(long applicationId)
    {
        return  _context.ApplicationPackages
            .Where(ap => ap.ApplicationId == applicationId)
            .Select(ap => ap.Id)
            .ToList();
    }


}
