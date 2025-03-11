using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

    public class ActeeRepository : IActeeRepository
{
    private readonly AuthDbContext dbContext;

    public ActeeRepository(AuthDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Actee>> GetActeesByIdsAsync(List<long> acteeIds)
    {
        return await dbContext.Actees
            .Where(a => acteeIds.Contains(a.Id))
            .ToListAsync();
    }
        public async Task<List<Actee>> GetActeesByApplicationPackageIdAsync(long applicationPackageId)
    {
        return await dbContext.Actees
                             .Where(a => a.ApplicationPackageId == applicationPackageId)
                             .ToListAsync();
    }

    public Task<Actee> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Actee> GetByUuidAsync(string uuid)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Actee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Actee>> GetByStatusAsync(StatusTypes status)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Actee>> GetByActeeTypeAsync(ActeeTypes acteeType)
    {
        throw new NotImplementedException();
    }

    public Task<Actee?> GetActeeByApplicationId(long applicationId)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Actee actee)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Actee actee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
