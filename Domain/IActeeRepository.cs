
using Domain.Entities;
using Domain.Enums;

namespace Domain;
    public interface IActeeRepository
    {
        Task<Actee> GetByIdAsync(long id);
        Task<Actee> GetByUuidAsync(string uuid);
        Task<IEnumerable<Actee>> GetAllAsync();
        Task<IEnumerable<Actee>> GetByStatusAsync(StatusTypes status);
        Task<IEnumerable<Actee>> GetByActeeTypeAsync(ActeeTypes acteeType);
        Task<Actee?> GetActeeByApplicationId(long applicationId); 
        Task AddAsync(Actee actee);
        Task UpdateAsync(Actee actee);
        Task DeleteAsync(long id);
        Task<int> SaveChangesAsync();
    }