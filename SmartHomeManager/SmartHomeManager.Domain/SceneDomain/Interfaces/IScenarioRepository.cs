using System;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IScenarioRepository<T> : IGenericRepository<T> where T : class
    {
        //public async Task<bool> AddAsync(Scenario scenario);

        //public async Task<bool> DeleteAsync(Scenario scenario);

        //public async Task<bool> DeleteByIdAsync(Guid id);

        //public async Task<IEnumerable<Scenario>> GetAllAsync();

        //public async Task<Scenario?> GetByIdAsync(Guid id);

        public Task<IEnumerable<Scenario?>> GetByProfileId(Guid id);
        public Task<Scenario?> GetScenarioByProfileId(Guid id);
        public Task<IEnumerable<Scenario>> GetByShareable();

        //public async Task<bool> SaveAsync();

        //public async Task<bool> UpdateAsync(Scenario scenario);
    }
}

