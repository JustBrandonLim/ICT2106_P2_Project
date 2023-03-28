using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IProfileRepository
    {
        public Task<bool> AddAsync(Profile profile);
        public Task<Profile?> GetByIdAsync(Guid id);
        public Task<IEnumerable<Profile>> GetAllAsync();
        public Task<int> UpdateAsync(Profile profile);
        public bool Delete(Profile profile);
        public Task<bool> DeleteByIdAsync(Guid id);
        public Task<IEnumerable<Profile>> GetProfilesByAccountId(Guid accountId);
        public Task<IEnumerable<Guid>> GetDevicesByProfileId(Guid profileId);
        public Task<int> SaveAsync();
    }
}
