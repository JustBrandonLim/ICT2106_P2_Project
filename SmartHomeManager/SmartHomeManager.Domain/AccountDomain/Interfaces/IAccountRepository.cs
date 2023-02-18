using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IAccountRepository
    {
        public Task<bool> AddAsync(Account account);
        public Task<Account?> GetByIdAsync(Guid id);
        public Task<IEnumerable<Account>> GetAllAsync();
        public Task<Account?> GetAccountByEmailAsync(string email);
        public Task<bool> IsEmailUnique(string email);
        public bool Update(Account account);
        public bool Delete(Account account);
        public Task<int> SaveAsync();

    }
}
