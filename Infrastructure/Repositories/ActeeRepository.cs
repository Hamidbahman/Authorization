using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain;

namespace DataAccess.Repositories
{
    public class ActeeRepository : IActeeRepository
    {
        private readonly DbContext _context;

        public ActeeRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Actee> GetByIdAsync(long id)
        {
            return await _context.Set<Actee>().FindAsync(id);
        }

        public async Task<Actee> GetByUuidAsync(string uuid)
        {
            return await _context.Set<Actee>().FirstOrDefaultAsync(a => a.Uuid == uuid);
        }

        public async Task<IEnumerable<Actee>> GetAllAsync()
        {
            return await _context.Set<Actee>().ToListAsync();
        }

        public async Task<IEnumerable<Actee>> GetByStatusAsync(StatusTypes status)
        {
            return await _context.Set<Actee>().Where(a => a.StatusType == status).ToListAsync();
        }

        public async Task<IEnumerable<Actee>> GetByActeeTypeAsync(ActeeTypes acteeType)
        {
            return await _context.Set<Actee>().Where(a => a.ActeeType == acteeType).ToListAsync();
        }

        public async Task AddAsync(Actee actee)
        {
            await _context.Set<Actee>().AddAsync(actee);
        }

        public async Task UpdateAsync(Actee actee)
        {
            EntityEntry<Actee> entityEntry = _context.Entry(actee);
            entityEntry.State = EntityState.Modified;
        }

        public async Task DeleteAsync(long id)
        {
            var actee = await GetByIdAsync(id);
            if (actee != null)
            {
                _context.Set<Actee>().Remove(actee);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
