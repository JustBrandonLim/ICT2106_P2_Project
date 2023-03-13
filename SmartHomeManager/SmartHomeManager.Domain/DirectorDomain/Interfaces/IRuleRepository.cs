using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IRuleRepository<T>
    {
        public Task<bool> AddAsync(T entity);
        public Task<bool> DeleteAsync(T entity);
        public Task<bool> DeleteByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(Guid id);
        public Task<bool> SaveAsync();
        public Task<bool> UpdateAsync(T entity);
        public Task<IEnumerable<T>> GetByScenarioId(Guid id);
    }
}
